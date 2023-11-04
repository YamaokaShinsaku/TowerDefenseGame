using UnityEngine;

/// <summary>
/// �v���C���[���j�b�g�̔z�u�֘A�̏������s��
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManager�̃C���X�^���X
    public static BuildManager instance;

    // �z�u����v���C���[���j�b�g
    //public GameObject playerUnitToBuild;
    private PlayerUnitData unitDataToBuild;

    // �e�v���C���[���j�b�g��Prefab
    public GameObject playerUnit1Prefab;
    public GameObject playerUnit2Prefab;

    // �v���C���[���j�b�g��ݒu�ł��邩�ǂ���
    public bool CanBuildPlayerUnit { get { return unitDataToBuild != null; } }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("BuildManager�����łɑ��݂��Ă��܂�");
            return;
        }
        instance = this;
    }

    /// <summary>
    /// �ݒu����v���C���[���j�b�g��I��
    /// </summary>
    /// <param name="playerunitDataToBuild">PlayerUnitData</param>
    public void SelectPlayerUnitToBuild(PlayerUnitData playerunitDataToBuild)
    {
        unitDataToBuild = playerunitDataToBuild;
    }

    /// <summary>
    /// �w���Node��Ƀv���C���[���j�b�g��ݒu
    /// </summary>
    /// <param name="node">Node</param>
    public void BuildPlayerUnitOnNode(Node node)
    {
        // �z�u����v���C���[���j�b�g���擾
        GameObject playerUnit =
            Instantiate(unitDataToBuild.prefab, node.transform.position + node.builPositonOffset, node.transform.rotation);
        node.playerUnit = playerUnit;

        // �A���Ŕz�u�ł��Ȃ��悤�ɂ��邽��playerUnitToBuild��NULL������
        unitDataToBuild = null;
    }
}
