using UnityEngine;

/// <summary>
/// 敵の移動管理
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // 移動先のオブジェクト
    private Transform target;
    // 現在の移動先オブジェクトの番号
    private int movePointIndex = 0;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = MovePoints.movePoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // 移動先ベクトルを計算し、移動させる
        Vector3 direction = target.position - this.transform.position;
        this.transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        // targetに十分に近づいたとき
        if (Vector3.Distance(this.transform.position, target.position) <= 0.2f)
        {
            GetNextMovePoint();
        }
        // 移動速度の初期化
        //enemy.speed = enemy.startSpeed;
    }

    /// <summary>
    /// 次のMovePointを取得
    /// </summary>
    void GetNextMovePoint()
    {
        // 最後のMovePointに到達したとき
        if (movePointIndex >= MovePoints.movePoints.Length - 1)
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
}
