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

    // コストの増加速度
    public float costInterval = 2.0f;

    private void Start()
    {
        cost = startCost;
        life = startLife;
        StartCoroutine(AddCost());
    }

    private void Update()
    {
        // Costが上限を超えないように
        if (cost > limitCost)
        {
            cost = limitCost;
            return;
        }
    }

    /// <summary>
    /// コストを時間経過で増加させる
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddCost()
    {
        while (true)
        {
            // ２秒待機
            yield return new WaitForSeconds(costInterval);
            // コストを増加させる
            if (cost < limitCost)
            {
                cost++;
            }
        }
    }
}
