using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̈ړ��Ǘ�
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // �ړ���̃I�u�W�F�N�g
    private Transform target;
    // ���݂̈ړ���I�u�W�F�N�g�̔ԍ�
    [SerializeField]
    private int movePointIndex = 0;
    [SerializeField]
    // �g�p���郋�[�g�̃C���f�b�N�X
    private int routeIndex = 0;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        //target = MovePoints.movePoints[routeIndex][movePointIndex];
        SetNextTarget();
        routeIndex = enemy.routeIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ���x�N�g�����v�Z���A�ړ�������
        Vector3 direction = target.position - this.transform.position;
        this.transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        // target�ɏ\���ɋ߂Â����Ƃ�
        if (Vector3.Distance(this.transform.position, target.position) <= 0.2f)
        {
            GetNextMovePoint();
        }
        // �ړ����x�̏�����
        //enemy.speed = enemy.startSpeed;
        Debug.Log(movePointIndex);
    }

    // ���̃^�[�Q�b�g��ݒ肷�郁�\�b�h
    private void SetNextTarget()
    {
        List<Transform> currentRoutePoints = MovePoints.movePoints[enemy.routeIndex];
        target = currentRoutePoints[movePointIndex];
    }

    /// <summary>
    /// ����MovePoint���擾
    /// </summary>
    void GetNextMovePoint()
    {
        // ���݂̃��[�g���ŁA�S�ړ��|�C���g�̐����擾����
        List<Transform> currentRoutePoints = MovePoints.movePoints[enemy.routeIndex];

        // �Ō��MovePoint�ɓ��B�����Ƃ�
        if (movePointIndex >= currentRoutePoints.Count - 1)
        {
            Debug.Log($"{movePointIndex}{currentRoutePoints.Count - 1}");
            EndPoint();
            return;
        }

        movePointIndex++;
        // target�Ɏ���MovePoint��ݒ�
        //target = currentRoutePoints[movePointIndex];
        SetNextTarget();
    }

    /// <summary>
    /// �I�_�ɓ��B�����Ƃ��̏���
    /// </summary>
    void EndPoint()
    {
        // ���C�t�����Z
        PlayerStats.life--;
        Debug.Log("Enemy Goal");
        // ���g���폜
        Destroy(this.gameObject);
    }
}
