using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// アセットやオブジェクトを登録し、呼び出せるようにする拡張機能
/// </summary>
public class FavoritesWindow : EditorWindow
{
    [MenuItem("MyTools/Favorites Window")]
    static void ShowWindow()
    {
        GetWindow<FavoritesWindow>("Favorites");
    }

    Vector2 scrollView;             // スクロールビューの位置
    AssetInfo lastOpenedAsset;      // 最後に開いたアセット情報

    /// <summary>
    /// データ構造
    /// </summary>
    [System.Serializable]
    class AssetInfo
    {
        public string guid; // アセットのGUID
        public string path; // アセットのパス 
        public string name; // アセットの名前
        public string type; // アセットのタイプ
    }

    // アセット情報のリスト
    [System.Serializable]
    class AssetInfoList
    {
        public List<AssetInfo> infoList = new List<AssetInfo>();
    }
    // アセットキャッシュ
    [SerializeField]
    private static AssetInfoList assetsCache = null;
    static AssetInfoList _assets
    {
        get
        {
            // キャッシュが null ならロードする
            if (assetsCache == null)
            {
                assetsCache = LoadPrefs();
            }
            return assetsCache;
        }
    }

    /// <summary>
    /// 保存・読み込み
    /// </summary>
    static string PrefsKey()
    {
        // ユニークなキーを探す
        return $"{Application.productName}-Favs";
    }
    static void SavePrefs()
    {
        // アセット情報をJSONに変換して保存する
        string prefsJson = JsonUtility.ToJson(_assets);
        EditorPrefs.SetString(PrefsKey(), prefsJson);
    }

    static AssetInfoList LoadPrefs()
    {
        Debug.Log("Loading Favorites Prefs ...");
        string prefsKey = PrefsKey();
        if (!EditorPrefs.HasKey(prefsKey))
        {
            return new AssetInfoList();
        }
        // 保存されたJSONデータを読み込み、オブジェクトに変換する
        string prefsJson = EditorPrefs.GetString(prefsKey);
        var assets = JsonUtility.FromJson<AssetInfoList>(prefsJson);
        if (assets == null)
        {
            Debug.LogError("Favorites Prefs Load Error");
            return new AssetInfoList();
        }

        return assets;
    }

    /// <summary>
    /// GUI描画
    /// </summary>
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            var content = new GUIContent("Fav", "Bookmark selected asset");
            // ブックマークに追加
            if (GUILayout.Button(content, GUILayout.Width(100), GUILayout.Height(40)))
            {
                BookmarkAsset();
            }
            GUILayout.BeginVertical();
            {
                // タイプでソート
                if (GUILayout.Button("▼ Sort by Type", GUILayout.MaxWidth(200)))
                {
                    SortByType();
                }
                // 名前でソート
                if (GUILayout.Button("▼ Sort by Name", GUILayout.MaxWidth(200)))
                {
                    SortByName();
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

        scrollView = GUILayout.BeginScrollView(scrollView);
        {
            // 削除するアイテムのリスト
            List<AssetInfo> itemsToRemove = null;

            foreach (var info in _assets.infoList)
            {
                GUILayout.BeginHorizontal();
                {
                    bool isCanceled = DrawAssetRow(info);
                    if (isCanceled)
                    {
                        // 直接削除せず、後でまとめて削除する
                        if (itemsToRemove == null)
                        {
                            itemsToRemove = new List<AssetInfo>();
                        }
                        itemsToRemove.Add(info);
                        // DrawAssetRow終了後にEndHorizontalを呼ぶためループを抜けない
                    }
                }
                GUILayout.EndHorizontal();
            }

            // 削除アイテムのリストを削除する
            if (itemsToRemove != null)
            {
                foreach (var info in itemsToRemove)
                {
                    RemoveAsset(info);
                }
            }
        }
        GUILayout.EndScrollView();
    }

    /// <summary>
    /// アセット行を描画する
    /// </summary>
    bool DrawAssetRow(AssetInfo info)
    {
        bool isCanceled = false;
        {
            var content = new GUIContent("◎", "Highlight asset");
            // アセットをハイライト表示する
            if (GUILayout.Button(content, GUILayout.ExpandWidth(false)))
            {
                HighlightAsset(info);
            }
        }
        {
            // アセットのボタンを描画
            DrawAssetItemButton(info);
        }
        {
            var content = new GUIContent("X", "Remove from Favs");
            // 削除フラグを設定
            if (GUILayout.Button(content, GUILayout.ExpandWidth(false)))
            {
                //RemoveAsset(info);
                isCanceled = true;
            }
        }
        return isCanceled;
    }
    /// <summary>
    /// アセットアイテムのボタンを描画する
    /// </summary>
    void DrawAssetItemButton(AssetInfo info)
    {
        var content = new GUIContent($"{info.name}", AssetDatabase.GetCachedIcon(info.path));
        var style = GUI.skin.button;
        // 元のアライメントを保存
        var originalAlignment = style.alignment;
        // 元のフォントスタイルを保存
        var originalFontStyle = style.fontStyle;
        // 元のテキストカラーを保存
        var originalTextColor = style.normal.textColor;
        style.alignment = TextAnchor.MiddleLeft;

        if (info == lastOpenedAsset)
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.yellow;
        }

        float width = position.width - 70f;
        // アセットを開く
        if (GUILayout.Button(content, style, GUILayout.MaxWidth(width), GUILayout.Height(18)))
        {
            OpenAsset(info);
        }

        // 元のスタイルを復元
        style.alignment = originalAlignment;
        style.fontStyle = originalFontStyle;
        style.normal.textColor = originalTextColor;
    }

    /// <summary>
    /// 内部処理
    /// </summary>

    /// <summary>
    /// ブックマークに追加する
    /// </summary>
    void BookmarkAsset()
    {
        foreach (string assetGuid in Selection.assetGUIDs)
        {
            if (_assets.infoList.Exists(x => x.guid == assetGuid))
            {
                continue;
            }
            // アセット情報を取得してリストに追加
            var info = new AssetInfo();
            info.guid = assetGuid;
            info.path = AssetDatabase.GUIDToAssetPath(assetGuid);
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(info.path);
            info.name = asset.name;
            info.type = asset.GetType().ToString();
            _assets.infoList.Add(info);
        }
        SavePrefs();
    }
    /// <summary>
    /// アセットを削除する
    /// </summary>
    void RemoveAsset(AssetInfo info)
    {
        _assets.infoList.Remove(info);
        SavePrefs();
    }
    /// <summary>
    /// アセットをハイライト表示する
    /// </summary>
    void HighlightAsset(AssetInfo info)
    {
        var asset = AssetDatabase.LoadAssetAtPath<Object>(info.path);
        EditorGUIUtility.PingObject(asset);
    }
    /// <summary>
    /// アセットを開く
    /// </summary>
    void OpenAsset(AssetInfo info)
    {
        // フォルダでない場合、最後に開いたアセットとして記録する
        if (info.type != "UnityEditor.DefaultAsset")
        {
            lastOpenedAsset = info;
        }
        // シーンアセットを開く
        if (Path.GetExtension(info.path).Equals(".unity"))
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(info.path, OpenSceneMode.Single);
            }
            return;
        }
        // その他のアセットを開く
        var asset = AssetDatabase.LoadAssetAtPath<Object>(info.path);
        AssetDatabase.OpenAsset(asset);
    }
    /// <summary>
    /// タイプでソートする
    /// </summary>
    void SortByType()
    {
        SortByName();
        _assets.infoList.Sort((a, b) =>
        {
            return a.type.CompareTo(b.type);
        });
    }
    /// <summary>
    /// 名前でソートする
    /// </summary>
    void SortByName()
    {
        _assets.infoList.Sort((a, b) =>
        {
            return a.name.CompareTo(b.name);
        });
    }
}
