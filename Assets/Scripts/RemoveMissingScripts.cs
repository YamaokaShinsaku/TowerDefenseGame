using UnityEngine;
//GameObjectUtilityを使うのに必要(※エディタ上でしか使えない)
using UnityEditor;

/// <summary>
/// MissingScriptを一択削除する
/// </summary>
public class RemoveMissingScripts : MonoBehaviour
{
    // シーン中に存在するMissingなスクリプトを削除する
    [MenuItem("Project/RemoveMissingScripts/Scene", false, 3)]
    public static void DeleteInScene()
    {
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        for (int i = 0; i < all.Length; ++i)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(all[i]);
        }
        Debug.Log("シーン中のMissingなスクリプトの削除が完了しました");
    }

    // プレハブに含まれるMissingなスクリプトを削除する
    [MenuItem("Project/RemoveMissingScripts/Prefab", false, 3)]
    public static void DeleteInPrefab()
    {
        string[] allGUID = AssetDatabase.FindAssets("t:prefab");
        for (int i = 0; i < allGUID.Length; ++i)
        {
            string path = AssetDatabase.GUIDToAssetPath(allGUID[i]);
            GameObject g = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(g);
        }
        AssetDatabase.Refresh();
        Debug.Log("全プレハブ中のMissingなスクリプトの削除が完了しました");
    }
}
