using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �G�̃f�[�^�Ǘ�
/// </summary>
public class Enemy : MonoBehaviour
{
    public EnemyUnitData enemyUnitData;

    // �̗�
    public float startHealth = 10;
    private float health;
    // �����ړ����x
    public float startSpeed = 10.0f;
    // �ړ����x
    public float speed = 10.0f;
    // ���[�g
    public int routeIndex = 0;
    // ���S���̃G�t�F�N�g
    public GameObject dieEffect;
    // �̗̓Q�[�W
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        enemyUnitData.SetRouteBasedOnType();
        routeIndex = enemyUnitData.routeIndex;
    }

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    /// <param name="damageValue">�_���[�W��</param>
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        healthBar.fillAmount = health / startHealth;
        //Debug.Log("�_���[�W���󂯂�");
        // �̗͂�0�ɂȂ����Ƃ�
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// �U�����󂯂��ۂɁA��莞�Ԍ�������
    /// </summary>
    /// <param name="decelerationValue">������</param>
    public void Slow(float decelerationValue)
    {
        speed = startSpeed * (1.0f - decelerationValue);

        Invoke("InitMoveSpeed", 0.5f);
    }

    /// <summary>
    /// �ړ����x�̏�����
    /// </summary>
    public void InitMoveSpeed()
    {
        speed = startSpeed;
    }

    /// <summary>
    /// �������@���󂯂�
    /// </summary>
    /// <param name="decelerationValue">������</param>
    public void TakeSlowMagic(float decelerationValue)
    {
        speed = startSpeed * (1.0f - decelerationValue);
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
