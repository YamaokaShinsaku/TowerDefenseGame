using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーのコスト,HP管理などを行う
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // プレイヤーが持っているコスト
    public static int cost;
    // 初期コスト
    public int startCost = 10;

    private void Start()
    {
        cost = startCost;
    }
}
