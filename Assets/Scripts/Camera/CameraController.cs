using UnityEngine;

/// <summary>
/// �J�����̈ړ�������s��
/// </summary>
public class CameraController : MonoBehaviour
{
    // �J�����𓮂����邩�ǂ���
    public bool doMovement = true;

    // �J�����̕��ʏ�̍��E�ړ����x
    public float moveSpeed = 30.0f;
    // �X�N���[�����x
    public float scrollSpeed = 5.0f;
    // �X�N���[�����̏���A����
    public float minScrollY = 20.0f;
    public float maxScrollY = 50.0f;

    // ��ʂ̒[����10�s�N�Z���ȓ��̔���Ɏg�p
    //public float screenBorderThickness = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // �J�����̓����Ԃ̐ؑ�
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if(!doMovement)
        {
            return;
        }

        if(Input.GetKey("w") 
            /*|| Input.mousePosition.y >= Screen.height - screenBorderThickness*/)
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s")
            /*|| Input.mousePosition.y <= screenBorderThickness*/)
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d")
           /* || Input.mousePosition.x >= Screen.width - screenBorderThickness*/)
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a")
            /*|| Input.mousePosition.x <= screenBorderThickness*/)
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }

        // �Y�[���C���A�Y�[���A�E�g����
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = this.transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // �X�N���[�����̏���A������ݒ�
        pos.y = Mathf.Clamp(pos.y, minScrollY, maxScrollY);
        // �J����Y���W�̍X�V
        this.transform.position = pos;
    }
}
