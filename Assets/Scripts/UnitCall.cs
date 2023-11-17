using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �z�u���郆�j�b�g�̎擾���s��
/// </summary>
public class UnitCall : MonoBehaviour
{
    // �e�v���C���[���j�b�g�̃f�[�^��ݒ�
    //public PlayerUnitData unit1Data;
    //public PlayerUnitData unit2Data;
    public PlayerUnitData[] unitDatas;
    // �e�v���C���[���j�b�g�̎g�p�R�X�g��\������UI
    public Text[] unitUseCostText;

    private void Start()
    {
        // �g�p�R�X�g��\��
        for (int i = 0; i < unitDatas.Length; i++)
        {
            unitUseCostText[i].text = unitDatas[i].cost.ToString();
        }
    }

    /// <summary>
    /// �w��̔ԍ���PlayerUnit���擾
    /// </summary>
    /// <param name="unitNumber"></param>
    public void SelectUnit(int unitNumber)
    {
        BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[unitNumber-1]);
    }
    ///// <summary>
    ///// PlayerUnit1���擾
    ///// </summary>
    //public void SelectPlayerUnit1()
    //{
    //    Debug.Log("Get PlayerUnit_1");
    //    //BuildManager.instance.SelectPlayerUnitToBuild(unit1Data);
    //    BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[0]);
    //}

    ///// <summary>
    ///// PlayerUnit2���擾
    ///// </summary>
    //public void SelectPlayerUnit2()
    //{
    //    Debug.Log("Get PlayerUnit_2");
    //    //BuildManager.instance.SelectPlayerUnitToBuild(unit2Data);
    //    BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[1]);
    //}

}
