using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyUnitData;

/// <summary>
/// Enemyのデータを設定する
/// </summary>
[System.Serializable]
public class EnemyUnitData
{
    public enum EnemyType
    {
        Type1,
        Type2,
    }
    [HideInInspector]
    public int routeIndex = 0;
    public EnemyType enemyType;

    /// <summary>
    /// EnemyUnitDataの初期設定をルートに応じて行う
    /// </summary>
    public void SetRouteBasedOnType()
    {
        // ルートを敵の種類に基づいて設定
        switch (enemyType)
        {
            case EnemyType.Type1:
                routeIndex = 0; // ルート1
                break;
            case EnemyType.Type2:
                routeIndex = 1; // ルート2
                break;
            default:
                routeIndex = 0; // デフォルトのルート
                break;
        }
    }
}
