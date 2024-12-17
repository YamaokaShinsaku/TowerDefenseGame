using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    // ��������G�l�~�[�̎��
    public EnemyUnitData.EnemyType enemyType;
    // ��������G�l�~�[�̍ő吔
    public int enemyCount;
    // �G�l�~�[�𐶐�����Ԋu
    public float spawnIntarval;
    // ����̃E�F�[�u�J�n�܂ł̊Ԋu
    public float waveIntarval;
    // �G�l�~�[�̃��[�g
    public int enemyRoute;

    public WaveData(EnemyUnitData.EnemyType enemyType, int enemyCount, float spawnIntarval, int routeIndex)
    {
        this.enemyType = enemyType;
        this.enemyCount = enemyCount;
        this.spawnIntarval = spawnIntarval;
        enemyRoute = routeIndex;
    }
}
