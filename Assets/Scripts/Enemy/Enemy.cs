using UnityEngine;

/// <summary>
/// 敵のデータ管理
/// </summary>
public class Enemy : MonoBehaviour
{
    // 体力
    public int health = 10;
    // 移動速度
    public float speed = 10.0f;
    // 死亡時のエフェクト
    public GameObject dieEffect;


    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damageValue">ダメージ量</param>
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        Debug.Log("ダメージを受けた");
        // 体力が0になったとき
        if(health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡時の処理
    /// </summary>
    void Die()
    {
        // 死亡エフェクトの再生
        Instantiate(dieEffect, this.transform.position, Quaternion.identity);
        // コストの加算
        PlayerStats.cost++;
        // 自身を削除
        Destroy(this.gameObject);
    }
}
