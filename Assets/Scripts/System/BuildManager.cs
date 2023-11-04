using UnityEngine;

/// <summary>
/// �v���C���[���j�b�g�̔z�u�֘A�̏������s��
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManager�̃C���X�^���X
    public static BuildManager instance;

    // �z�u����v���C���[���j�b�g
    public GameObject playerUnitToBuild;

    // �v���C���[���j�b�g
    public GameObject playerUnit1Prefab;
    public GameObject playerUnit2Prefab;

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
    /// �z�u����v���C���[���j�b�g���擾����
    /// </summary>
    /// <returns>�v���C���[���j�b�g</returns>
    public GameObject GetPlayerUnitToBuild()
    { 
        return playerUnitToBuild; 
    }

    /// <summary>
    /// PlayerUnit_1���擾
    /// </summary>
    /// <param name="playerUnit"></param>
    public void SetPlayerUnit1ToBuild(GameObject playerUnit)
    {
        playerUnitToBuild = playerUnit;
    }

    /// <summary>
    /// PlayerUnit_2���擾
    /// </summary>
    /// <param name="playerUnit"></param>
    public void SetPlayerUnit2ToBuild(GameObject playerUnit)
    {
        playerUnitToBuild = playerUnit;
    }
}
