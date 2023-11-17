using UnityEngine;

/// <summary>
/// �G�̃f�[�^�Ǘ�
/// </summary>
public class Enemy : MonoBehaviour
{
    // �̗�
    public int health = 10;
    // �ړ����x
    public float speed = 10.0f;
    // ���S���̃G�t�F�N�g
    public GameObject dieEffect;


    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="damageValue">�_���[�W��</param>
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        Debug.Log("�_���[�W���󂯂�");
        // �̗͂�0�ɂȂ����Ƃ�
        if(health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// ���S���̏���
    /// </summary>
    void Die()
    {
        // ���S�G�t�F�N�g�̍Đ�
        Instantiate(dieEffect, this.transform.position, Quaternion.identity);
        // �R�X�g�̉��Z
        PlayerStats.cost++;
        // ���g���폜
        Destroy(this.gameObject);
    }
}
