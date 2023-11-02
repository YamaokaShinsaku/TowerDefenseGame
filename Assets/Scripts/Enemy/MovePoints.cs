using UnityEngine;

/// <summary>
/// EnemyのMovePointを取得する
/// </summary>
public class MovePoints : MonoBehaviour
{
    // 移動先となるTransformをすべて取得
    public static Transform[] movePoints;

    private void Awake()
    {
        // 子の数分の配列を用意する
        movePoints = new Transform[this.transform.childCount];
        for(int i = 0; i < movePoints.Length; i++) 
        {
            // 用意した配列に、子オブジェクトを格納
            movePoints[i] = this.transform.GetChild(i);
        }
    }
}
