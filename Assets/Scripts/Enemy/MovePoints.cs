using UnityEngine;

/// <summary>
/// Enemy��MovePoint���擾����
/// </summary>
public class MovePoints : MonoBehaviour
{
    // �ړ���ƂȂ�Transform�����ׂĎ擾
    public static Transform[] movePoints;

    private void Awake()
    {
        // �q�̐����̔z���p�ӂ���
        movePoints = new Transform[this.transform.childCount];
        for(int i = 0; i < movePoints.Length; i++) 
        {
            // �p�ӂ����z��ɁA�q�I�u�W�F�N�g���i�[
            movePoints[i] = this.transform.GetChild(i);
        }
    }
}
