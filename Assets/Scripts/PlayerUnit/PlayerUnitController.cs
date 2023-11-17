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
/// �v���C���[���j�b�g�̍U�����@
/// </summary>
public enum AttackType
{
    Sword,      // ��
    SlowMagic,  // �������@
}

/// <summary>
/// �v���C���[���j�b�g�̍s������
/// </summary>
public class PlayerUnitController : MonoBehaviour
{
    [Header("Unit Setting")]
    // ���݂̍U���Ώ�
    //[SerializeField]
    //private GameObject target;
    // �U���͈�
    //public float range = 15.0f;
    // �U����
    public int attackValue = 1;

    // �U�����x�i���[�g�j
    public float attackRate = 1.0f;
    // �U���܂ł̃J�E���g�_�E��
    [SerializeField]
    private float attackCountDown = 0.0f;
    // �G�̌�����
    public float decelerationRate = 0.5f;

    // �U���G�t�F�N�g
    public GameObject attackEffect;
    // �U���G�t�F�N�g�̐����ꏊ
    [SerializeField]
    private Transform createAttackEffectTransform;
    // �v���C���[���j�b�g�̌����Ă������
    public PlayerUnitDirection  direction;
    // �U���^�C�v
    public AttackType attackType;
    // �����I���{�^���������ꂽ���ǂ���
    public bool pushDirectionButton = false;

    public Animator animator;

    [Header("No need to touch")]
    // �G�L�����N�^�[�̃^�O
    public string enemyTag = "Enemy";
    // �����I���{�^��
    public GameObject directionButtons;

    // �U���͈͓��ɂ���G���i�[����List
    [SerializeField]
    List<GameObject> enemiesInAttackArea = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        Time.timeScale = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // �z����̃I�u�W�F�N�g��Null�ɂȂ�΍폜����
        // (�G���U���͈͓���Destroy���ꂽ�Ƃ��̂��߂̏���)
        for (int i = 0; i < enemiesInAttackArea.Count; i++)
        {
            if (enemiesInAttackArea[i] == null)
            {
                enemiesInAttackArea.RemoveAt(i);
            }
        }

        if (enemiesInAttackArea == null || enemiesInAttackArea.Count == 0)
        {
            animator.SetBool("isSlow", false);
            if(enemiesInAttackArea.Count <= 1)
            {
                attackCountDown = 0.0f;
                return;
            }
            return;
        }

        // ���ōU������ꍇ
        if (attackType == AttackType.Sword)
        {
            attackCountDown -= Time.deltaTime;
            if (attackCountDown <= 0.0f)
            {
                attackCountDown = 0.0f;
            }
            // �U���Ώۂ����݂��Ȃ��ꍇ�́A�ȍ~�������Ȃ�
            if (enemiesInAttackArea == null || enemiesInAttackArea.Count == 0)
            {
                return;
            }

            if (pushDirectionButton)
            {
                // �U�����s��
                if (attackCountDown <= 0.0f /*&& enemiesInAttackArea.Count != 0*/)
                {
                    Attack();
                    attackCountDown = 1.0f / attackRate;
                }
            }
            //attackCountDown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// �U������
    /// </summary>
    void Attack()
    {
        //Debug.Log(this.gameObject.name + "Attack");
        // �U���A�j���[�V�������Đ�
        animator.SetTrigger("isAttack");
        // �U���G�t�F�N�g�𐶐�
        Instantiate(attackEffect, createAttackEffectTransform);
        // �z����̓G�Ƀ_���[�W��^����
        foreach (GameObject enemies in enemiesInAttackArea)
        {
            // �G�����݂��Ă����
            if(enemies != null)
            {
                enemies.GetComponent<Enemy>().TakeDamage(attackValue);
                enemies.GetComponent<Enemy>().Slow(decelerationRate);
            }
        }
        // �U�����[�g�����Z�b�g
        attackCountDown = 0.0f;
    }

    //void Magic()
    //{
    //    //Debug.Log(this.gameObject.name + "Attack");
    //    // �U���A�j���[�V�������Đ�
    //    animator.SetTrigger("isAttack");
    //    //// �U���G�t�F�N�g�𐶐�
    //    //Instantiate(attackEffect, createAttackEffectTransform);
    //    // �z����̓G�Ƀ_���[�W��^����
    //    foreach (GameObject enemies in enemiesInAttackArea)
    //    {
    //        // �G�����݂��Ă����
    //        if (enemies != null)
    //        {
    //            enemies.GetComponent<Enemy>().TakeSlowMagic(decelerationRate);
    //        }
    //    }
    //    //// �U�����[�g�����Z�b�g
    //    //attackCountDown = 0.0f;
    //}

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

    // ����������s���֐�(�{�^������Ă΂��)
    public void SetFront()
    {
        direction = PlayerUnitDirection.Front;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetBack()
    {
        direction = PlayerUnitDirection.Back;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetRight()
    {
        direction = PlayerUnitDirection.Right;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }
    public void SetLeft()
    {
        direction = PlayerUnitDirection.Left;
        SetPlayerUnitDirection(direction);
        Time.timeScale = 1.0f;
        pushDirectionButton = true;
    }

    // �U���͈͓��ɓG�������ė�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = other.gameObject;
            // �G��z��ɒǉ�
            enemiesInAttackArea.Add(other.gameObject);
            // �������@���g�p����ꍇ
            if(attackType == AttackType.SlowMagic)
            {
                // �U���A�j���[�V�������Đ�
                //animator.SetTrigger("isAttack");
                animator.SetBool("isSlow", true);
                // �z����̓G�̑��x������������
                for (int i = 0; i < enemiesInAttackArea.Count; i++)
                {
                    if (enemiesInAttackArea[i] != null)
                    {
                        enemiesInAttackArea[i].GetComponent<Enemy>().TakeSlowMagic(decelerationRate);
                    }
                }
            }
        }
    }
    // �U���͈͓�����G���o����
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            // �������@���g�p����ꍇ
            if (attackType == AttackType.SlowMagic)
            {
                enemiesInAttackArea[0].GetComponent<Enemy>().InitMoveSpeed();
            }
            //target = null;
            // �z�񂩂�폜����
            enemiesInAttackArea.RemoveAt(0);
        }
    }

    ///// <summary>
    ///// �U���Ώۂ̍X�V���s��
    ///// </summary>
    //void UpdateTarget()
    //{
    //    // enemyTag�����I�u�W�F�N�g��z��Ɋi�[
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    // ����܂łɌ������G�܂ł̍ŒZ����
    //    float shortestDistance = Mathf.Infinity;
    //    // �ł��߂��G
    //    GameObject nearestEnemy = null;

    //    foreach (GameObject enemy in enemies)
    //    {
    //        // �����ƃG�l�~�[�Ƃ̋������v�Z
    //        float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
    //        // �������G�����݂̍ŒZ�������߂��ʒu�ɂ���Ƃ�
    //        if(distanceToEnemy < shortestDistance)
    //        {
    //            // nearestEnemy,shortestDistance�̍X�V���s��
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    // �U���͈͓��ɓG�����݂��Ă���Ƃ�
    //    if (nearestEnemy != null && shortestDistance <= range)
    //    {
    //        // �U���Ώۂ�ݒ�
    //        target = nearestEnemy;
    //    }
    //    else
    //    {
    //        target = null;
    //    }
    //}

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
