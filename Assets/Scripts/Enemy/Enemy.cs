using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ړ����x
    public float speed = 10.0f;
    // �ړ���̃I�u�W�F�N�g
    private Transform target;
    // ���݂̈ړ���I�u�W�F�N�g�̔ԍ�
    private int movePointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = MovePoints.movePoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ���x�N�g�����v�Z���A�ړ�������
        Vector3 direction = target.position - this.transform.position;
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // target�ɏ\���ɋ߂Â����Ƃ�
        if(Vector3.Distance(this.transform.position, target.position) <= 0.2f)
        {
            GetNextMovePoint();
        }
    }

    /// <summary>
    /// ����MovePoint���擾
    /// </summary>
    void GetNextMovePoint()
    {
        // �Ō��MovePoint�ɓ��B�����Ƃ�
        if(movePointIndex >= MovePoints.movePoints.Length - 1)
        {
            Destroy(this.gameObject);
            return;
        }

        movePointIndex++;
        // target�Ɏ���MovePoint��ݒ�
        target = MovePoints.movePoints[movePointIndex];
    }
}
