using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̌���
/// </summary>
public enum PlayerUnitDirection
{
    Front,  // �O
    Back,   // ���
    Right,  // �E
    Left    // ��
}

/// <summary>
/// �v���C���[���j�b�g�̍s������
/// </summary>
public class PlayerUnitController : MonoBehaviour
{
    [Header("Unit Setting")]
    // ���݂̍U���Ώ�
    [SerializeField]
    private GameObject target;
    // �U���͈�
    public float range = 15.0f;
    //public Vector3 range;

    // �U�����x�i���[�g�j
    public float attackRate = 1.0f;
    // �U���܂ł̃J�E���g�_�E��
    [SerializeField]
    private float attackCountDown = 0.0f;

    // �U���G�t�F�N�g
    public GameObject attackEffect;
    // �U���G�t�F�N�g�̐����ꏊ
    [SerializeField]
    private Transform createAttackEffectTransform;
    // �v���C���[���j�b�g�̌����Ă������
    public PlayerUnitDirection  direction;

    public Animator animator;

    [Header("No need to touch")]
    // �G�L�����N�^�[�̃^�O
    public string enemyTag = "Enemy";

    // �U���͈͓��ɂ���G���i�[����List
    [SerializeField]
    List<GameObject> enemiesInAttackArea = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        SetPlayerUnitDirection(direction);
    }

    // Update is called once per frame
    void Update()
    {
        // �U���Ώۂ����݂��Ȃ��ꍇ�́A�ȍ~�������Ȃ�
        //if (!target)
        //{
        //    return;
        //}
        if (enemiesInAttackArea == null || enemiesInAttackArea.Count == 0)
        {
            // �U�����[�g�����Z�b�g
            attackCountDown = 0.0f;
            return;
        }

        // �U�����s��
        if (attackCountDown <= 0.0f�@&& enemiesInAttackArea.Count != 0)
        {
            Attack();
            attackCountDown = 1.0f / attackRate;
        }
        attackCountDown -= Time.deltaTime;
    }

    /// <summary>
    /// �U������
    /// </summary>
    void Attack()
    {
        Debug.Log(this.gameObject.name + "Attack");
        // �U���A�j���[�V�������Đ�
        animator.SetTrigger("isAttack");
        // �U���G�t�F�N�g�𐶐�
        Instantiate(attackEffect, createAttackEffectTransform);
    }

    /// <summary>
    /// �v���C���[�̌�����ݒ�
    /// </summary>
    public void SetPlayerUnitDirection(PlayerUnitDirection direction)
    {
        switch (direction)
        {
            case PlayerUnitDirection.Front:
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case PlayerUnitDirection.Back:
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case PlayerUnitDirection.Right:
                this.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case PlayerUnitDirection.Left:
                this.transform.eulerAngles = new Vector3(0, -90, 0);
                break;
        }
    }

    // �U���͈͓��ɓG�������ė�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = other.gameObject;
            enemiesInAttackArea.Add(other.gameObject);
        }
    }
    // �U���͈͓�����G���o����
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = null;
            enemiesInAttackArea.RemoveAt(0);
        }
    }

    /// <summary>
    /// �U���Ώۂ̍X�V���s��
    /// </summary>
    void UpdateTarget()
    {
        // enemyTag�����I�u�W�F�N�g��z��Ɋi�[
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // ����܂łɌ������G�܂ł̍ŒZ����
        float shortestDistance = Mathf.Infinity;
        // �ł��߂��G
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            // �����ƃG�l�~�[�Ƃ̋������v�Z
            float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
            // �������G�����݂̍ŒZ�������߂��ʒu�ɂ���Ƃ�
            if(distanceToEnemy < shortestDistance)
            {
                // nearestEnemy,shortestDistance�̍X�V���s��
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // �U���͈͓��ɓG�����݂��Ă���Ƃ�
        if (nearestEnemy != null && shortestDistance <= range)
        {
            // �U���Ώۂ�ݒ�
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    /// <summary>
    /// �V�[����ɍU���͈͂̃M�Y����\������
    /// </summary>
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //    //Gizmos.DrawWireCube(transform.position, range);
    //}
}
