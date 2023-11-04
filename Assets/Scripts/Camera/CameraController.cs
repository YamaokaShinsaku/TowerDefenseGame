using UnityEngine;

/// <summary>
/// カメラの移動制御を行う
/// </summary>
public class CameraController : MonoBehaviour
{
    // カメラを動かせるかどうか
    public bool doMovement = true;

    // カメラの平面上の左右移動速度
    public float moveSpeed = 30.0f;
    // スクロール速度
    public float scrollSpeed = 5.0f;
    // スクロール時の上限、下限
    public float minScrollY = 20.0f;
    public float maxScrollY = 50.0f;

    // 画面の端から10ピクセル以内の判定に使用
    //public float screenBorderThickness = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // カメラの動作状態の切替
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

        // ズームイン、ズームアウト処理
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = this.transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // スクロール時の上限、下限を設定
        pos.y = Mathf.Clamp(pos.y, minScrollY, maxScrollY);
        // カメラY座標の更新
        this.transform.position = pos;
    }
}
