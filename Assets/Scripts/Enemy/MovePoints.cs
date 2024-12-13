using UnityEngine;

/// <summary>
/// EnemyのMovePointを取得する
/// </summary>
public class MovePoints : MonoBehaviour
{
    // 移動先となるTransformをすべて取得
    public static Transform[][] movePoints;

    private void Awake()
    {
        // ルートの数を取得
        int routeCount = this.transform.childCount;
        // 配列を初期化
        movePoints = new Transform[routeCount][];

        // 各ルートの移動ポイントを取得する
        for (int i = 0; i < routeCount; i++)
        {
            // 現在のルートの子オブジェクトを取得
            Transform route = this.transform.GetChild(i);
            int pointCount = route.childCount;

            // ルートごとに配列を用意する
            movePoints[i] = new Transform[pointCount];

            for (int j = 0; j < pointCount; j++)
            {
                // 用意した配列にルートの子オブジェクトを格納
                movePoints[i][j] = route.GetChild(i);
            }
        }


        //// 子の数分の配列を用意する
        //movePoints = new Transform[this.transform.childCount];
        //for (int i = 0; i < movePoints.Length; i++)
        //{
        //    // 用意した配列に、子オブジェクトを格納
        //    movePoints[i] = this.transform.GetChild(i);
        //}
    }

    // 例: 指定したルートのポイントを取得するメソッド
    public static Transform[] GetMovePoints(int routeIndex)
    {
        if (routeIndex < 0 || routeIndex >= movePoints.Length)
        {
            Debug.LogError("Invalid route index");
            return null; // 無効なインデックスの場合はnullを返す
        }
        return movePoints[routeIndex];
    }
}
