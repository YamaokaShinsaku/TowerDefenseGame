using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーユニットのデータを設定する
/// </summary>
[System.Serializable]
public class PlayerUnitData
{
    // プレイヤーユニットのPrefab
    public GameObject prefab;
    // 設置に必要なコスト
    public int cost;
}

