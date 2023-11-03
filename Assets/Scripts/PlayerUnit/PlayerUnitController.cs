using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���j�b�g�̍s������
/// </summary>
public class PlayerUnitController : MonoBehaviour
{
    // ���݂̍U���Ώ�
    [SerializeField]
    private GameObject target;
    // �U���͈�
    public float range = 15.0f;
    //public Vector3 range;

    // �G�L�����N�^�[�̃^�O
    public string enemyTag = "Enemy";

    // �U���͈͓��ɂ���G���i�[����List
    [SerializeField]
    List<GameObject> enemiesInAttackArea = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
        {
            return;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = other.gameObject;
            enemiesInAttackArea.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            //target = null;
            enemiesInAttackArea.RemoveAt(0);
        }
    }
}
