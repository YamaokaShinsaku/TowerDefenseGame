using System.Collections;
using UnityEngine;

/// <summary>
/// �v���C���[�̃R�X�g,HP�Ǘ��Ȃǂ��s��
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // �v���C���[�������Ă���R�X�g
    public static int cost;
    // cost�̏��
    static int limitCost = 99;
    // �����R�X�g
    public int startCost = 10;

    public static int life;
    public int startLife = 20;

    private void Start()
    {
        cost = startCost;
        life = startLife;
    }

    private void Update()
    {
        // Cost������𒴂��Ȃ��悤��
        if(cost > limitCost)
        {
            cost = limitCost;
        }
    }
}
