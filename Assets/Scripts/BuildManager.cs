using UnityEngine;

/// <summary>
/// �v���C���[���j�b�g�̔z�u�֘A�̏������s��
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManager�̃C���X�^���X
    public static BuildManager instance;

    // �z�u����v���C���[���j�b�g
    private GameObject playerUnitToBuild;

    // �f�t�H���g�̃v���C���[���j�b�g
    public GameObject standardPlayerUnitPrefab;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("BuildManager�����łɑ��݂��Ă��܂�");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        playerUnitToBuild = standardPlayerUnitPrefab;
    }

    /// <summary>
    /// �z�u����v���C���[���j�b�g���擾����
    /// </summary>
    /// <returns>�v���C���[���j�b�g</returns>
    public GameObject GetPlayerUnitToBuild()
    { 
        return playerUnitToBuild; 
    }
}
