using UnityEngine;

public enum Sequence
{
    SelectDirection,    // �v���C���[���j�b�g�̌�����I��
    BuildPlayerUnit,    // �v���C���[���j�b�g��z�u
    None
}

/// <summary>
/// �u���b�N��ɉ������\�z����Ă��邩�𒲂ׂ�
/// </summary>
public class Node : MonoBehaviour
{
    // �I�����ꂽ�Ƃ���Color
    public Color hoverColor;
    // ������Color��ۑ�����
    private Color startColor;

    private Vector3 builPositonOffset = new Vector3(0, 0.5f, 0);

    private Renderer renderer;

    Sequence sequence;

    // �u���b�N��ɑ��݂���v���C���[���j�b�g
    private GameObject playerUnit;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    /// <summary>
    /// �I�u�W�F�N�g�I�𒆂Ƀ}�E�X�{�^���������ꂽ�Ƃ��Ă΂��
    /// </summary>
    private void OnMouseDown()
    {
        // �v���C���[���j�b�g�����݂���ꍇ
        if(playerUnit != null) 
        {
            Debug.Log("�����ɂ͔z�u�ł��܂���");
            return;
        }

        // �z�u����v���C���[���j�b�g���擾
        GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
        playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    }

    /// <summary>
    /// �I�u�W�F�N�g�Ƀ}�E�X���d�Ȃ��Ă���Ƃ��ɌĂ΂��
    /// </summary>
    private void OnMouseEnter()
    {
        // Color��ύX����
        renderer.material.color = hoverColor;
    }

    /// <summary>
    /// �}�E�X���I�u�W�F�N�g���痣�ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    private void OnMouseExit()
    {
        // Color������Color�ɖ߂�
        renderer.material.color = startColor;
    }
}
