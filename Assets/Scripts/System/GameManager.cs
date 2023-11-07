using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���̐i�s�Ǘ����s��
/// </summary>
public class GameManager : MonoBehaviour
{
    // �Q�[�����I���������ǂ���
    private bool isGameEnd = false;

    // Update is called once per frame
    void Update()
    {
        if(isGameEnd)
        {
            return;
        }

        // ���C�t��0�ɂȂ����Ƃ�
        if(PlayerStats.life <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// �Q�[���I�[�o�[���̏���
    /// </summary>
    void GameOver()
    {
        isGameEnd = true;
        Debug.Log("Game Over");
    }
}
