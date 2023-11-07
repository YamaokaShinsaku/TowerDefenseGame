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
    // 移動先のオブジェクト
    private Transform target;
    // 現在の移動先オブジェクトの番号
    private int movePointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = MovePoints.movePoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // 移動先ベクトルを計算し、移動させる
        Vector3 direction = target.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // targetに十分に近づいたとき
        if(Vector3.Distance(this.transform.position, target.position) <= 0.2f)
        {
            GetNextMovePoint();
        }
    }

    /// <summary>
    /// 次のMovePointを取得
    /// </summary>
    void GetNextMovePoint()
    {
        // 最後のMovePointに到達したとき
        if(movePointIndex >= MovePoints.movePoints.Length - 1)
        {
            EndPoint();
            return;
        }

        movePointIndex++;
        // targetに次のMovePointを設定
        target = MovePoints.movePoints[movePointIndex];
    }

    /// <summary>
    /// 終点に到達したときの処理
    /// </summary>
    void EndPoint()
    {
        // ライフを減算
        PlayerStats.life--;
        // 自身を削除
        Destroy(this.gameObject);
    }

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
        PlayerStats.cost++;
        Destroy(this.gameObject);
    }
}
