using System.Collections;
using UnityEngine;

/// <summary>
/// �v���C���[�̃R�X�g,HP�Ǘ��Ȃǂ��s��
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // �v���C���[�������Ă���R�X�g
    public static int cost;
    // �����R�X�g
    public int startCost = 10;

    private void Start()
    {
        cost = startCost;
    }
}
