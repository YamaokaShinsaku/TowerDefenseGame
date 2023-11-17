using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの向き
/// </summary>
public enum PlayerUnitDirection
{
    Front,  // 前
    Back,   // 後ろ
    Right,  // 右
    Left    // 左
}

/// <summary>
/// プレイヤーユニットの攻撃方法
/// </summary>
public enum AttackType
{
    Sword,      // 剣
    SlowMagic,  // 減速魔法
}

/// <summary>
/// プレイヤーユニットの行動制御
/// </summary>
public class PlayerUnitController : MonoBehaviour
{
    [Header("Unit Setting")]
    // 現在の攻撃対象
    //[SerializeField]
    //private GameObject target;
    // 攻撃範囲
    //public float range = 15.0f;
    // 攻撃力
    public int attackValue = 1;

    // 攻撃速度（レート）
    public float attackRate = 1.0f;
    // 攻撃までのカウントダウン
    [SerializeField]
    private float attackCountDown = 0.0f;
    // 敵の減速率
    public float decelerationRate = 0.5f;

    // 攻撃エフェクト
    public GameObject attackEffect;
    // 攻撃エフェクトの生成場所
    [SerializeField]
    private Transform createAttackEffectTransform;
    // プレイヤーユニットの向いている方向
    public PlayerUnitDirection  direction;
    // 攻撃タイプ
    public AttackType attackType;
    // 方向選択ボタンが押されたかどうか
    public bool pushDirectionButton = false;

    public Animator animator;

    [Header("No need to touch")]
    // 敵キャラクターのタグ
    public string enemyTag = "Enemy";
    // 方向選択ボタン
    public GameObject directionButtons;

    // 攻撃範囲内にいる敵を格納するList
    [SerializeField]
    List<GameObject> enemiesInAttackArea = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        Time.timeScale = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // 配列内のオブジェクトがNullになれば削除する
        // (敵が攻撃範囲内でDestroyされたときのための処理)
        for (int i = 0; i < enemiesInAttackArea.Count; i++)
        {
            if (enemiesInAttackArea[i] == null)
            {
                enemiesInAttackArea.RemoveAt(i);
            }
        }

        if (enemiesInAttackArea == null || enemiesInAttackArea.Count == 0)
        {
            animator.SetBool("isSlow", false);
            if(enemiesInAttackArea.Count <= 1)
            {
                attackCountDown = 0.0f;
                return;
            }
            return;
        }

        // 剣で攻撃する場合
        if (attackType == AttackType.Sword)
        {
            attackCountDown -= Time.deltaTime;
            if (attackCountDown <= 0.0f)
            {
                attackCountDown = 0.0f;
            }
            // 攻撃対象が存在しない場合は、以降処理しない
            if (enemiesInAttackArea == null || enemiesInAttackArea.Count == 0)
            {
                return;
            }

            if (pushDirectionButton)
            {
                // 攻撃を行う
                if (attackCountDown <= 0.0f /*&& enemiesInAttackArea.Count != 0*/)
                {
                    Attack();
                    attackCountDown = 1.0f / attackRate;
                }
            }
            //attackCountDown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    void Attack()
    {
        //Debug.Log(this.gameObject.name + "Attack");
        // 攻撃アニメーションを再生
        animator.SetTrigger("isAttack");
        // 攻撃エフェクトを生成
        Instantiate(attackEffect, createAttackEffectTransform);
        // 配列内の敵にダメージを与える
        foreach (GameObject enemies in enemiesInAttackArea)
        {
            // 敵が存在していれば
            if(enemies != null)
            {
                enemies.GetComponent<Enemy>().TakeDamage(attackValue);
                enemies.GetComponent<Enemy>().Slow(decelerationRate);
            }
        }
        // 攻撃レートをリセット
        attackCountDown = 0.0f;
    }

    //void Magic()
    //{
    //    //Debug.Log(this.gameObject.name + "Attack");
    //    // 攻撃アニメーションを再生
    //    animator.SetTrigger("isAttack");
    //    //// 攻撃エフェクトを生成
    //    //Instantiate(attackEffect, createAttackEffectTransform);
    //    // 配列内の敵にダメージを与える
    //    foreach (GameObject enemies in enemiesInAttackArea)
    //    {
    //        // 敵が存在していれば
    //        if (enemies != null)
    //        {
    //            enemies.GetComponent<Enemy>().TakeSlowMagic(decelerationRate);
    //        }
    //    }
    //    //// 攻撃レートをリセット
    //    //attackCountDown = 0.0f;
    //}

    /// <summary>
    /// プレイヤーの向きを設定
    /// </summary>
    public void SetPlayerUnitDirection(PlayerUnitDirection direction)
    {
        switch (direction)
        {
            case PlayerUnitDirection.Front:
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case PlayerUnitDirection.Back:
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case PlayerUnitDirection.Right:
                this.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case PlayerUnitDirection.Left:
                this.transform.eulerAngles = new Vector3(0, -90, 0);
                break;
        }
    }

    // 方向決定を行う関数(ボタンから呼ばれる)
    public void SetFront()
    {
        direction = PlayerUnitDirection.Front;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetBack()
    {
        direction = PlayerUnitDirection.Back;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetRight()
    {
        direction = PlayerUnitDirection.Right;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetLeft()
    {
        direction = PlayerUnitDirection.Left;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }

    // 攻撃範囲内に敵が入って来た時
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = other.gameObject;
            // 敵を配列に追加
            enemiesInAttackArea.Add(other.gameObject);
            // 減速魔法を使用する場合
            if(attackType == AttackType.SlowMagic)
            {
                // 攻撃アニメーションを再生
                //animator.SetTrigger("isAttack");
                animator.SetBool("isSlow", true);
                // 配列内の敵の速度を減少させる
                for (int i = 0; i < enemiesInAttackArea.Count; i++)
                {
                    if (enemiesInAttackArea[i] != null)
                    {
                        enemiesInAttackArea[i].GetComponent<Enemy>().TakeSlowMagic(decelerationRate);
                    }
                }
            }
        }
    }
    // 攻撃範囲内から敵が出た時
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            // 減速魔法を使用する場合
            if (attackType == AttackType.SlowMagic)
            {
                enemiesInAttackArea[0].GetComponent<Enemy>().InitMoveSpeed();
            }
            //target = null;
            // 配列から削除する
            enemiesInAttackArea.RemoveAt(0);
        }
    }

    ///// <summary>
    ///// 攻撃対象の更新を行う
    ///// </summary>
    //void UpdateTarget()
    //{
    //    // enemyTagを持つオブジェクトを配列に格納
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    // これまでに見つけた敵までの最短距離
    //    float shortestDistance = Mathf.Infinity;
    //    // 最も近い敵
    //    GameObject nearestEnemy = null;

    //    foreach (GameObject enemy in enemies)
    //    {
    //        // 自分とエネミーとの距離を計算
    //        float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
    //        // 見つけた敵が現在の最短距離より近い位置にいるとき
    //        if(distanceToEnemy < shortestDistance)
    //        {
    //            // nearestEnemy,shortestDistanceの更新を行う
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    // 攻撃範囲内に敵が存在しているとき
    //    if (nearestEnemy != null && shortestDistance <= range)
    //    {
    //        // 攻撃対象を設定
    //        target = nearestEnemy;
    //    }
    //    else
    //    {
    //        target = null;
    //    }
    //}

    /// <summary>
    /// シーン上に攻撃範囲のギズモを表示する
    /// </summary>
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //    //Gizmos.DrawWireCube(transform.position, range);
    //}
}
