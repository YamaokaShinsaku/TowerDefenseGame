using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyUnitData;

/// <summary>
/// Enemy�̃f�[�^��ݒ肷��
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
    /// EnemyUnitData�̏����ݒ�����[�g�ɉ����čs��
    /// </summary>
    public void SetRouteBasedOnType()
    {
        // ���[�g��G�̎�ނɊ�Â��Đݒ�
        switch (enemyType)
        {
            case EnemyType.Type1:
                routeIndex = 0; // ���[�g1
                break;
            case EnemyType.Type2:
                routeIndex = 1; // ���[�g2
                break;
            default:
                routeIndex = 0; // �f�t�H���g�̃��[�g
                break;
        }
    }
}
