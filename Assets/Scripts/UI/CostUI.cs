using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���݂̃R�X�g��\��
/// </summary>
public class CostUI : MonoBehaviour
{
    // ���݂̃R�X�g�ʂ�\������Text
    public Text costText;

    // Update is called once per frame
    void Update()
    {
        costText.text = PlayerStats.cost.ToString();
    }
}
