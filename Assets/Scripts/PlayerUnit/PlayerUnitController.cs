using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーユニットの行動制御
/// </summary>
public class PlayerUnitController : MonoBehaviour
{
    // 現在の攻撃対象
    [SerializeField]
    private GameObject target;
    // 攻撃範囲
    public float range = 15.0f;
    //public Vector3 range;

    // 敵キャラクターのタグ
    public string enemyTag = "Enemy";

    // 攻撃範囲内にいる敵を格納するList
    [SerializeField]
    List<GameObject> enemiesInAttackArea = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
        {
            return;
        }
    }

    /// <summary>
    /// 攻撃対象の更新を行う
    /// </summary>
    void UpdateTarget()
    {
        // enemyTagを持つオブジェクトを配列に格納
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // これまでに見つけた敵までの最短距離
        float shortestDistance = Mathf.Infinity;
        // 最も近い敵
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            // 自分とエネミーとの距離を計算
            float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
            // 見つけた敵が現在の最短距離より近い位置にいるとき
            if(distanceToEnemy < shortestDistance)
            {
                // nearestEnemy,shortestDistanceの更新を行う
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // 攻撃範囲内に敵が存在しているとき
        if (nearestEnemy != null && shortestDistance <= range)
        {
            // 攻撃対象を設定
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    /// <summary>
    /// シーン上に攻撃範囲のギズモを表示する
    /// </summary>
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //    //Gizmos.DrawWireCube(transform.position, range);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = other.gameObject;
            enemiesInAttackArea.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = null;
            enemiesInAttackArea.RemoveAt(0);
        }
    }
}
