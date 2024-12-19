using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class HierarchyMemoColorEditor : EditorWindow
{
    // メモと色を保持するディクショナリー
    private static Dictionary<int, (string memo, Color color)> objectSettings = new Dictionary<int, (string, Color)>();
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
        ("Orange", new Color(1.0f, 0.647f, 0.0f)) // RGB値でオレンジ
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
    }

    private void OnDisable()
    {
        // 不要になったので、選択が変更されたときのイベントを解除
        Selection.selectionChanged -= OnSelectionChange;
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
                color = Color.white; // デフォルト色
            }
        }
        else
        {
            memo = "Enter memo here...";
            color = Color.white; // デフォルト色
        }

        Repaint(); // ウィンドウを再描画
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a GameObject in the Hierarchy", EditorStyles.boldLabel);

        if (selectedObject != null)
        {
            int instanceID = selectedObject.GetInstanceID();

            // 色の設定
            EditorGUILayout.BeginVertical("box"); // 色の設定部分を囲む枠
            GUILayout.Label("Color Presets", EditorStyles.boldLabel); // 太字で表示

            // 横並びにプリセットを表示
            EditorGUILayout.BeginHorizontal();
            foreach (var preset in colorPresets)
            {
                // カラーボックスを表示
                Rect boxRect = GUILayoutUtility.GetRect(30, 30, GUILayout.ExpandWidth(true)); // 30pxx30pxのボックスを設定
                EditorGUI.DrawRect(boxRect, preset.color); // ボックスを描画

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
            EditorGUILayout.EndVertical(); // 色の設定部分を囲んだ枠を終了

            // 分割線
            EditorGUI.DrawRect(EditorGUILayout.BeginHorizontal(), Color.black);
            GUILayout.Space(5);
            EditorGUILayout.EndHorizontal();

            // Memoの表示
            EditorGUILayout.BeginVertical("box"); // メモ部分を囲む枠
            GUILayout.Label("Memo", EditorStyles.boldLabel); // 太字で表示
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(100)); // スクロールエリアの高さを設定
            memo = EditorGUILayout.TextArea(memo, GUILayout.Height(200)); // メモの入力エリアを広くする
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical(); // メモ部分を囲んだ枠を終了

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
        EditorUtility.SetDirty(selectedObject); // オブジェクトをdirtyにする
        Debug.Log($"Applied memo and color to {selectedObject.name}");
    }

    private void ResetSettings(int instanceID)
    {
        // メモを空に、色をリセット
        objectSettings.Remove(instanceID); // 設定を削除することで背景色をデフォルトに戻す
        EditorUtility.SetDirty(selectedObject); // オブジェクトをdirtyにする
        Debug.Log($"Reset settings for {selectedObject.name}");
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
}