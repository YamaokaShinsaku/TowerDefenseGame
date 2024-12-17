using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    // 生成するエネミーの種類
    public EnemyUnitData.EnemyType enemyType;
    // 生成するエネミーの最大数
    public int enemyCount;
    // エネミーを生成する間隔
    public float spawnIntarval;
    // 次回のウェーブ開始までの間隔
    public float waveIntarval;
    // エネミーのルート
    public int enemyRoute;

    public WaveData(EnemyUnitData.EnemyType enemyType, int enemyCount, float spawnIntarval, int routeIndex)
    {
        this.enemyType = enemyType;
        this.enemyCount = enemyCount;
        this.spawnIntarval = spawnIntarval;
        enemyRoute = routeIndex;
    }
}
