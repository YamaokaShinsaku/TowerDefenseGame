using UnityEngine;

/// <summary>
/// �z�u���郆�j�b�g�̎擾���s��
/// </summary>
public class UnitCall : MonoBehaviour
{
    // �e�v���C���[���j�b�g�̃f�[�^��ݒ�
    public PlayerUnitData unit1Data;
    public PlayerUnitData unit2Data;

    /// <summary>
    /// PlayerUnit1���擾
    /// </summary>
    public void SelectPlayerUnit1()
    {
        Debug.Log("Get PlayerUnit_1");
        BuildManager.instance.SelectPlayerUnitToBuild(unit1Data);
    }

    /// <summary>
    /// PlayerUnit2���擾
    /// </summary>
    public void SelectPlayerUnit2()
    {
        Debug.Log("Get PlayerUnit_2");
        BuildManager.instance.SelectPlayerUnitToBuild(unit2Data);
    }
}
