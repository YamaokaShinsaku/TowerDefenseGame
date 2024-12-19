using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// �A�Z�b�g��I�u�W�F�N�g��o�^���A�Ăяo����悤�ɂ���g���@�\
/// </summary>
public class FavoritesWindow : EditorWindow
{
    [MenuItem("MyTools/Favorites Window")]
    static void ShowWindow()
    {
        GetWindow<FavoritesWindow>("Favorites");
    }

    Vector2 scrollView;             // �X�N���[���r���[�̈ʒu
    AssetInfo lastOpenedAsset;      // �Ō�ɊJ�����A�Z�b�g���

    /// <summary>
    /// �f�[�^�\��
    /// </summary>
    [System.Serializable]
    class AssetInfo
    {
        public string guid; // �A�Z�b�g��GUID
        public string path; // �A�Z�b�g�̃p�X 
        public string name; // �A�Z�b�g�̖��O
        public string type; // �A�Z�b�g�̃^�C�v
    }

    // �A�Z�b�g���̃��X�g
    [System.Serializable]
    class AssetInfoList
    {
        public List<AssetInfo> infoList = new List<AssetInfo>();
    }
    // �A�Z�b�g�L���b�V��
    [SerializeField]
    private static AssetInfoList assetsCache = null;
    static AssetInfoList _assets
    {
        get
        {
            // �L���b�V���� null �Ȃ烍�[�h����
            if (assetsCache == null)
            {
                assetsCache = LoadPrefs();
            }
            return assetsCache;
        }
    }

    /// <summary>
    /// �ۑ��E�ǂݍ���
    /// </summary>
    static string PrefsKey()
    {
        // ���j�[�N�ȃL�[��T��
        return $"{Application.productName}-Favs";
    }
    static void SavePrefs()
    {
        // �A�Z�b�g����JSON�ɕϊ����ĕۑ�����
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
        // �ۑ����ꂽJSON�f�[�^��ǂݍ��݁A�I�u�W�F�N�g�ɕϊ�����
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
    /// GUI�`��
    /// </summary>
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            var content = new GUIContent("Fav", "Bookmark selected asset");
            // �u�b�N�}�[�N�ɒǉ�
            if (GUILayout.Button(content, GUILayout.Width(100), GUILayout.Height(40)))
            {
                BookmarkAsset();
            }
            GUILayout.BeginVertical();
            {
                // �^�C�v�Ń\�[�g
                if (GUILayout.Button("�� Sort by Type", GUILayout.MaxWidth(200)))
                {
                    SortByType();
                }
                // ���O�Ń\�[�g
                if (GUILayout.Button("�� Sort by Name", GUILayout.MaxWidth(200)))
                {
                    SortByName();
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

        scrollView = GUILayout.BeginScrollView(scrollView);
        {
            // �폜����A�C�e���̃��X�g
            List<AssetInfo> itemsToRemove = null;

            foreach (var info in _assets.infoList)
            {
                GUILayout.BeginHorizontal();
                {
                    bool isCanceled = DrawAssetRow(info);
                    if (isCanceled)
                    {
                        // ���ڍ폜�����A��ł܂Ƃ߂č폜����
                        if (itemsToRemove == null)
                        {
                            itemsToRemove = new List<AssetInfo>();
                        }
                        itemsToRemove.Add(info);
                        // DrawAssetRow�I�����EndHorizontal���ĂԂ��߃��[�v�𔲂��Ȃ�
                    }
                }
                GUILayout.EndHorizontal();
            }

            // �폜�A�C�e���̃��X�g���폜����
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
    /// �A�Z�b�g�s��`�悷��
    /// </summary>
    bool DrawAssetRow(AssetInfo info)
    {
        bool isCanceled = false;
        {
            var content = new GUIContent("��", "Highlight asset");
            // �A�Z�b�g���n�C���C�g�\������
            if (GUILayout.Button(content, GUILayout.ExpandWidth(false)))
            {
                HighlightAsset(info);
            }
        }
        {
            // �A�Z�b�g�̃{�^����`��
            DrawAssetItemButton(info);
        }
        {
            var content = new GUIContent("X", "Remove from Favs");
            // �폜�t���O��ݒ�
            if (GUILayout.Button(content, GUILayout.ExpandWidth(false)))
            {
                //RemoveAsset(info);
                isCanceled = true;
            }
        }
        return isCanceled;
    }
    /// <summary>
    /// �A�Z�b�g�A�C�e���̃{�^����`�悷��
    /// </summary>
    void DrawAssetItemButton(AssetInfo info)
    {
        var content = new GUIContent($"{info.name}", AssetDatabase.GetCachedIcon(info.path));
        var style = GUI.skin.button;
        // ���̃A���C�����g��ۑ�
        var originalAlignment = style.alignment;
        // ���̃t�H���g�X�^�C����ۑ�
        var originalFontStyle = style.fontStyle;
        // ���̃e�L�X�g�J���[��ۑ�
        var originalTextColor = style.normal.textColor;
        style.alignment = TextAnchor.MiddleLeft;

        if (info == lastOpenedAsset)
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.yellow;
        }

        float width = position.width - 70f;
        // �A�Z�b�g���J��
        if (GUILayout.Button(content, style, GUILayout.MaxWidth(width), GUILayout.Height(18)))
        {
            OpenAsset(info);
        }

        // ���̃X�^�C���𕜌�
        style.alignment = originalAlignment;
        style.fontStyle = originalFontStyle;
        style.normal.textColor = originalTextColor;
    }

    /// <summary>
    /// ��������
    /// </summary>

    /// <summary>
    /// �u�b�N�}�[�N�ɒǉ�����
    /// </summary>
    void BookmarkAsset()
    {
        foreach (string assetGuid in Selection.assetGUIDs)
        {
            if (_assets.infoList.Exists(x => x.guid == assetGuid))
            {
                continue;
            }
            // �A�Z�b�g�����擾���ă��X�g�ɒǉ�
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
    /// �A�Z�b�g���폜����
    /// </summary>
    void RemoveAsset(AssetInfo info)
    {
        _assets.infoList.Remove(info);
        SavePrefs();
    }
    /// <summary>
    /// �A�Z�b�g���n�C���C�g�\������
    /// </summary>
    void HighlightAsset(AssetInfo info)
    {
        var asset = AssetDatabase.LoadAssetAtPath<Object>(info.path);
        EditorGUIUtility.PingObject(asset);
    }
    /// <summary>
    /// �A�Z�b�g���J��
    /// </summary>
    void OpenAsset(AssetInfo info)
    {
        // �t�H���_�łȂ��ꍇ�A�Ō�ɊJ�����A�Z�b�g�Ƃ��ċL�^����
        if (info.type != "UnityEditor.DefaultAsset")
        {
            lastOpenedAsset = info;
        }
        // �V�[���A�Z�b�g���J��
        if (Path.GetExtension(info.path).Equals(".unity"))
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(info.path, OpenSceneMode.Single);
            }
            return;
        }
        // ���̑��̃A�Z�b�g���J��
        var asset = AssetDatabase.LoadAssetAtPath<Object>(info.path);
        AssetDatabase.OpenAsset(asset);
    }
    /// <summary>
    /// �^�C�v�Ń\�[�g����
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
    /// ���O�Ń\�[�g����
    /// </summary>
    void SortByName()
    {
        _assets.infoList.Sort((a, b) =>
        {
            return a.name.CompareTo(b.name);
        });
    }
}
