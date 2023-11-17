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
    private int movePointIndex = 0;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = MovePoints.movePoints[0];
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
    }

    /// <summary>
    /// ����MovePoint���擾
    /// </summary>
    void GetNextMovePoint()
    {
        // �Ō��MovePoint�ɓ��B�����Ƃ�
        if (movePointIndex >= MovePoints.movePoints.Length - 1)
        {
            EndPoint();
            return;
        }

        movePointIndex++;
        // target�Ɏ���MovePoint��ݒ�
        target = MovePoints.movePoints[movePointIndex];
    }

    /// <summary>
    /// �I�_�ɓ��B�����Ƃ��̏���
    /// </summary>
    void EndPoint()
    {
        // ���C�t�����Z
        PlayerStats.life--;
        // ���g���폜
        Destroy(this.gameObject);
    }
}
