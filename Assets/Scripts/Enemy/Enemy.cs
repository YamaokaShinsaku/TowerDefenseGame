using UnityEngine;

public class Enemy : MonoBehaviour
{
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
            Destroy(this.gameObject);
            return;
        }

        movePointIndex++;
        // targetに次のMovePointを設定
        target = MovePoints.movePoints[movePointIndex];
    }
}
