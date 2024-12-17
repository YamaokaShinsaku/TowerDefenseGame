using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵のデータ管理
/// </summary>
public class Enemy : MonoBehaviour
{
    public EnemyUnitData enemyUnitData;

    // 体力
    public float startHealth = 10;
    private float health;
    // 初期移動速度
    public float startSpeed = 10.0f;
    // 移動速度
    public float speed = 10.0f;
    // ルート
    public int routeIndex = 0;
    // 死亡時のエフェクト
    public GameObject dieEffect;
    // 体力ゲージ
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        enemyUnitData.SetRouteBasedOnType();
        routeIndex = enemyUnitData.routeIndex;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damageValue">ダメージ量</param>
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        healthBar.fillAmount = health / startHealth;
        //Debug.Log("ダメージを受けた");
        // 体力が0になったとき
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 攻撃を受けた際に、一定時間減速する
    /// </summary>
    /// <param name="decelerationValue">減速量</param>
    public void Slow(float decelerationValue)
    {
        speed = startSpeed * (1.0f - decelerationValue);

        Invoke("InitMoveSpeed", 0.5f);
    }

    /// <summary>
    /// 移動速度の初期化
    /// </summary>
    public void InitMoveSpeed()
    {
        speed = startSpeed;
    }

    /// <summary>
    /// 減速魔法を受ける
    /// </summary>
    /// <param name="decelerationValue">減速量</param>
    public void TakeSlowMagic(float decelerationValue)
    {
        speed = startSpeed * (1.0f - decelerationValue);
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
