using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーのコスト,HP管理などを行う
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // プレイヤーが持っているコスト
    public static int cost;
    // costの上限
    static int limitCost = 99;
    // 初期コスト
    public int startCost = 10;

    public static int life;
    public int startLife = 20;

    private void Start()
    {
        cost = startCost;
        life = startLife;
    }

    private void Update()
    {
        // Costが上限を超えないように
        if(cost > limitCost)
        {
            cost = limitCost;
        }
    }
}
