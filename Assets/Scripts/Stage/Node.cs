using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �u���b�N��ɉ������\�z����Ă��邩�𒲂ׂ�
/// </summary>
public class Node : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    // �I�����ꂽ�Ƃ���Color
    public Color hoverColor;
    // ������Color��ۑ�����
    private Color startColor;
    // �ݒu����ۂ̃I�t�Z�b�g���W
    public Vector3 builPositonOffset = new Vector3(0, 0.5f, 0);
    // �u���b�N��ɑ��݂���v���C���[���j�b�g
    public GameObject playerUnit;

    private Renderer renderer;

    //private GameObject clone;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    /// <summary>
    /// �I�u�W�F�N�g�I�𒆂Ƀ}�E�X�{�^���������ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // �v���C���[���j�b�g���ݒu�ł��Ȃ��ꍇ
        if (!BuildManager.instance.CanBuildPlayerUnit)
        {
            return;
        }

        // �v���C���[���j�b�g�����݂���ꍇ
        if (playerUnit != null)
        {
            Debug.Log("�����ɂ͔z�u�ł��܂���");
            return;
        }
        // �v���C���[���j�b�g��ݒu����
        BuildManager.instance.BuildPlayerUnitOnNode(this);
    }

    /// <summary>
    /// �J�[�\�����I�u�W�F�N�g�ɏd�Ȃ��Ă���Ƃ��ɌĂ΂��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �v���C���[���j�b�g���ݒu�ł��Ȃ��ꍇ
        if (!BuildManager.instance.CanBuildPlayerUnit)
        {
            return;
        }
        // Color��ύX����
        renderer.material.color = hoverColor;

        // �I������Ă���v���C���[���j�b�g���擾
        //GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
        //clone = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    }

    /// <summary>
    /// �J�[�\�����I�u�W�F�N�g���痣�ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Color������Color�ɖ߂�
        renderer.material.color = startColor;
        //Destroy(clone);
    }

    ///// <summary>
    ///// �I�u�W�F�N�g�I�𒆂Ƀ}�E�X�{�^���������ꂽ�Ƃ��Ă΂��
    ///// </summary>
    //private void OnMouseDown()
    //{
    //    // �v���C���[���j�b�g�����݂���ꍇ
    //    if(playerUnit != null) 
    //    {
    //        Debug.Log("�����ɂ͔z�u�ł��܂���");
    //        return;
    //    }

    //    // �z�u����v���C���[���j�b�g���擾
    //    GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
    //    playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    //}

    ///// <summary>
    ///// �I�u�W�F�N�g�Ƀ}�E�X���d�Ȃ��Ă���Ƃ��ɌĂ΂��
    ///// </summary>
    //private void OnMouseEnter()
    //{
    //    // Color��ύX����
    //    renderer.material.color = hoverColor;
    //}

    ///// <summary>
    ///// �}�E�X���I�u�W�F�N�g���痣�ꂽ�Ƃ��ɌĂ΂��
    ///// </summary>
    //private void OnMouseExit()
    //{
    //    // Color������Color�ɖ߂�
    //    renderer.material.color = startColor;
    //}

}
