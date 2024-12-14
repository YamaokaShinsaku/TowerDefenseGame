using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyのMovePointを取得する
/// </summary>
public class MovePoints : MonoBehaviour
{
    // 移動先となるTransformをすべて取得
    //public static Transform[][] movePoints;
    public static Dictionary<int, List<Transform>> movePoints;

    private void Awake()
    {
        // ルートの数を取得
        int routeCount = this.transform.childCount;
        // 配列を初期化
        movePoints = new Dictionary<int, List<Transform>>();

        // 各ルートの移動ポイントを取得する
        for (int i = 0; i < routeCount; i++)
        {
            // 現在のルートの子オブジェクトを取得
            Transform route = this.transform.GetChild(i);
            int pointCount = route.childCount;

            // ルートごとに配列を用意する
            //movePoints[i] = new Transform[pointCount];
            List<Transform> pointsList = new List<Transform>();

            for (int j = 0; j < pointCount; j++)
            {
                // 用意した配列にルートの子オブジェクトを格納
                //movePoints[i][j] = route.GetChild(i);
                pointsList.Add(route.GetChild(j));
            }

            // ルートのポイントリストをmovePointsに追加
            movePoints.Add(i, pointsList);
        }


        //// 子の数分の配列を用意する
        //movePoints = new Transform[this.transform.childCount];
        //for (int i = 0; i < movePoints.Length; i++)
        //{
        //    // 用意した配列に、子オブジェクトを格納
        //    movePoints[i] = this.transform.GetChild(i);
        //}
    }

    //// 例: 指定したルートのポイントを取得するメソッド
    //public static Transform[] GetMovePoints(int routeIndex)
    //{
    //    if (routeIndex < 0 || routeIndex >= movePoints.Length)
    //    {
    //        Debug.LogError("Invalid route index");
    //        return null; // 無効なインデックスの場合はnullを返す
    //    }
    //    return movePoints[routeIndex];
    //}
    // 例: 指定したルートのポイントを取得するメソッド
    public static List<Transform> GetMovePoints(int routeIndex)
    {
        if (!movePoints.ContainsKey(routeIndex))
        {
            Debug.LogError("Invalid route index");
            return null; // 無効なインデックスの場合はnullを返す
        }
        return movePoints[routeIndex];
    }
}
