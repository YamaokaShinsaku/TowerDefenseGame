using UnityEngine;

/// <summary>
/// Enemy��MovePoint���擾����
/// </summary>
public class MovePoints : MonoBehaviour
{
    // �ړ���ƂȂ�Transform�����ׂĎ擾
    public static Transform[][] movePoints;

    private void Awake()
    {
        // ���[�g�̐����擾
        int routeCount = this.transform.childCount;
        // �z���������
        movePoints = new Transform[routeCount][];

        // �e���[�g�̈ړ��|�C���g���擾����
        for (int i = 0; i < routeCount; i++)
        {
            // ���݂̃��[�g�̎q�I�u�W�F�N�g���擾
            Transform route = this.transform.GetChild(i);
            int pointCount = route.childCount;

            // ���[�g���Ƃɔz���p�ӂ���
            movePoints[i] = new Transform[pointCount];

            for (int j = 0; j < pointCount; j++)
            {
                // �p�ӂ����z��Ƀ��[�g�̎q�I�u�W�F�N�g���i�[
                movePoints[i][j] = route.GetChild(i);
            }
        }


        //// �q�̐����̔z���p�ӂ���
        //movePoints = new Transform[this.transform.childCount];
        //for (int i = 0; i < movePoints.Length; i++)
        //{
        //    // �p�ӂ����z��ɁA�q�I�u�W�F�N�g���i�[
        //    movePoints[i] = this.transform.GetChild(i);
        //}
    }

    // ��: �w�肵�����[�g�̃|�C���g���擾���郁�\�b�h
    public static Transform[] GetMovePoints(int routeIndex)
    {
        if (routeIndex < 0 || routeIndex >= movePoints.Length)
        {
            Debug.LogError("Invalid route index");
            return null; // �����ȃC���f�b�N�X�̏ꍇ��null��Ԃ�
        }
        return movePoints[routeIndex];
    }
}
