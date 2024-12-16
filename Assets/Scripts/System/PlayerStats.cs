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

    // �R�X�g�̑������x
    public float costInterval = 2.0f;

    private void Start()
    {
        cost = startCost;
        life = startLife;
        StartCoroutine(AddCost());
    }

    private void Update()
    {
        // Cost������𒴂��Ȃ��悤��
        if (cost > limitCost)
        {
            cost = limitCost;
            return;
        }
    }

    /// <summary>
    /// �R�X�g�����Ԍo�߂ő���������
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddCost()
    {
        while (true)
        {
            // �Q�b�ҋ@
            yield return new WaitForSeconds(costInterval);
            // �R�X�g�𑝉�������
            if (cost < limitCost)
            {
                cost++;
            }
        }
    }
}
