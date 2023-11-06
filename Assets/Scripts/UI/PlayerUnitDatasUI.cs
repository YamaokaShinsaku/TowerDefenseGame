using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[���j�b�g�̃f�[�^UI�̑�����s��
/// </summary>
public class PlayerUnitDatasUI : MonoBehaviour
{
    // �e�v���C���[���j�b�g�̃f�[�^UI
    public GameObject[] playerDatasUI;

    public static PlayerUnitDatasUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideAllPlayerUnitDatasUI();
        //for (int i = 0; i < playerDatasUI.Length; i++)
        //{
        //    playerDatasUI[i].SetActive(false);
        //}
    }

    /// <summary>
    /// �w��̃v���C���[���j�b�g�̃f�[�^UI��\������
    /// </summary>
    /// <param name="index">�\������UI�̔ԍ�</param>
    public void ShowPlayerUnitDatasUI(int index)
    {
        playerDatasUI[index].SetActive(true);
    }

    /// <summary>
    /// �w��̃v���C���[���j�b�g�̃f�[�^UI���\���ɂ���
    /// </summary>
    /// <param name="index">�\������UI�̔ԍ�</param>
    public void HidePlayerUnitDatasUI(int index)
    {
        // �w���UI���\������Ă���ꍇ
        if (playerDatasUI[index].activeInHierarchy)
        {
            playerDatasUI[index].SetActive(false);
        }
    }

    /// <summary>
    /// ���ׂẴv���C���[���j�b�g�̃f�[�^UI���\���ɂ���
    /// </summary>
    public void HideAllPlayerUnitDatasUI()
    {
        for (int i = 0; i < playerDatasUI.Length; i++)
        {
            if (playerDatasUI[i].activeInHierarchy)
            {
                playerDatasUI[i].SetActive(false);
            }
            //playerDatasUI[i].SetActive(false);
        }
    }
}
