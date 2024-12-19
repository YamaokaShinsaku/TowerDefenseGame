using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Hierarchy上のオブジェクトに背景色・メモを追加する拡張機能
/// </summary>
public class HierarchyMemoColorEditor : EditorWindow
{
    // メモと色を保持するディクショナリー
    private static Dictionary<int, (string memo, Color color)> objectSettings = new Dictionary<int, (string, Color)>();
    private const string settingsFileName = "hierarchyMemoColorSettings.json";

    private GameObject selectedObject;

    private string memo = "Enter memo here...";
    private Color color = Color.white;

    // 色プリセット
    private readonly (string name, Color color)[] colorPresets = new (string, Color)[]
    {
        ("Red", Color.red),
        ("Green", Color.green),
        ("Blue", Color.blue),
        ("Yellow", Color.yellow),
        ("Cyan", Color.cyan),
        ("Magenta", Color.magenta),
        ("Orange", new Color(1.0f, 0.647f, 0.0f))
    };

    // スクロールに使用する変数
    private Vector2 scrollPosition;

    [MenuItem("Window/Memo and Color Editor")]
    public static void ShowWindow()
    {
        GetWindow<HierarchyMemoColorEditor>("Memo and Color Editor");
    }

    // 右クリックメニューからウィンドウを開くためのメソッド
    [MenuItem("GameObject/Memo and Color Editor", false, 0)]
    public static void OpenWindow()
    {
        ShowWindow();
    }

    private void OnEnable()
    {
        // 選択が変更されるたびにOnSelectionChangeメソッドを呼び出す
        Selection.selectionChanged += OnSelectionChange;
        LoadSettings();
    }

    private void OnDisable()
    {
        // 不要になったので、選択が変更されたときのイベントを解除
        Selection.selectionChanged -= OnSelectionChange;
        SaveSettings();
    }

    private void OnSelectionChange()
    {
        // 選択されたオブジェクトを更新
        selectedObject = Selection.activeGameObject;
        if (selectedObject != null)
        {
            int instanceID = selectedObject.GetInstanceID();
            if (objectSettings.TryGetValue(instanceID, out var settings))
            {
                memo = settings.memo;
                color = settings.color;
            }
            else
            {
                memo = "Enter memo here...";
                color = Color.white;
            }
        }
        else
        {
            memo = "Enter memo here...";
            color = Color.white;
        }
        // ウィンドウを再描画
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a GameObject in the Hierarchy", EditorStyles.boldLabel);

        if (selectedObject != null)
        {
            int instanceID = selectedObject.GetInstanceID();

            // 色の設定
            // 色の設定部分を囲む枠
            EditorGUILayout.BeginVertical("box");
            // 太字で表示
            GUILayout.Label("Color Presets", EditorStyles.boldLabel);

            // 横並びにプリセットを表示
            EditorGUILayout.BeginHorizontal();
            foreach (var preset in colorPresets)
            {
                // カラーボックスを表示
                // 30pxx30pxのボックスを設定
                Rect boxRect = GUILayoutUtility.GetRect(30, 30, GUILayout.ExpandWidth(true));
                // ボックスを描画
                EditorGUI.DrawRect(boxRect, preset.color);

                // ボックスがクリックされたらその色を選択
                if (Event.current.type == EventType.MouseDown && boxRect.Contains(Event.current.mousePosition))
                {
                    color = preset.color;
                    // イベントを消費
                    Event.current.Use();
                }
            }
            EditorGUILayout.EndHorizontal();

            // Customのカラーピッカーを表示
            color = EditorGUILayout.ColorField("Custom Color", color);
            // 色の設定部分を囲んだ枠を終了
            EditorGUILayout.EndVertical();

            // 分割線
            EditorGUI.DrawRect(EditorGUILayout.BeginHorizontal(), Color.black);
            GUILayout.Space(5);
            EditorGUILayout.EndHorizontal();

            // Memoの表示
            // メモ部分を囲む枠
            EditorGUILayout.BeginVertical("box");
            // 太字で表示
            GUILayout.Label("Memo", EditorStyles.boldLabel);
            // スクロールエリアの高さを設定
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100));
            // メモの入力エリア
            memo = EditorGUILayout.TextArea(memo, GUILayout.Height(200));
            EditorGUILayout.EndScrollView();
            // メモ部分を囲んだ枠を終了
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Apply Settings"))
            {
                ApplySettings(instanceID);
            }

            // リセットボタンの追加
            if (GUILayout.Button("Reset Settings"))
            {
                ResetSettings(instanceID);
            }
        }
        else
        {
            GUILayout.Label("No GameObject selected!");
        }
    }

    private void ApplySettings(int instanceID)
    {
        objectSettings[instanceID] = (memo, color);
        // オブジェクトをdirtyにする
        EditorUtility.SetDirty(selectedObject);
        SaveSettings();
        Debug.Log($"Applied memo and color to {selectedObject.name}");
    }

    /// <summary>
    /// メモを空に、色をリセット
    /// </summary>
    /// <param name="instanceID"></param>
    private void ResetSettings(int instanceID)
    {
        // 設定を削除することで背景色をデフォルトに戻す
        objectSettings.Remove(instanceID);
        // オブジェクトをdirtyにする
        EditorUtility.SetDirty(selectedObject);
        SaveSettings();
        Debug.Log($"Reset settings for {selectedObject.name}");
    }

    private void SaveSettings()
    {
        string json = JsonUtility.ToJson(new SerializableDictionary(objectSettings));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, settingsFileName), json);
    }

    private void LoadSettings()
    {
        string path = Path.Combine(Application.persistentDataPath, settingsFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableDictionary data = JsonUtility.FromJson<SerializableDictionary>(json);
            objectSettings = data.ToDictionary();
        }
    }

    [InitializeOnLoad]
    public class HierarchyCustomEditor
    {
        static HierarchyCustomEditor()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;

            // 色の設定
            if (objectSettings.TryGetValue(instanceID, out var settings))
            {
                // 設定色で背景を塗る
                EditorGUI.DrawRect(selectionRect, settings.color);

                // 通常のアイコンと名前を表示します
                GUIContent labelContent = EditorGUIUtility.ObjectContent(obj, obj.GetType());
                GUI.Label(selectionRect, labelContent);
            }
        }
    }
    // 直列化可能なディクショナリを定義
    [System.Serializable]
    private class SerializableDictionary
    {
        public List<KeyValue> entries;

        public SerializableDictionary(Dictionary<int, (string memo, Color color)> dictionary)
        {
            entries = new List<KeyValue>();
            foreach (var kvp in dictionary)
            {
                entries.Add(new KeyValue { key = kvp.Key, memo = kvp.Value.memo, color = kvp.Value.color });
            }
        }

        public Dictionary<int, (string memo, Color color)> ToDictionary()
        {
            var dict = new Dictionary<int, (string memo, Color color)>();
            foreach (var entry in entries)
            {
                dict[entry.key] = (entry.memo, entry.color);
            }
            return dict;
        }
    }

    [System.Serializable]
    private class KeyValue
    {
        public int key;
        public string memo;
        public Color color;
    }

}