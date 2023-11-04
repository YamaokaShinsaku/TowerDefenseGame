using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ブロック上に何かが構築されているかを調べる
/// </summary>
public class Node : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    // 選択されたときのColor
    public Color hoverColor;
    // 初期のColorを保存する
    private Color startColor;
    // 接地する際のオフセット座標
    private Vector3 builPositonOffset = new Vector3(0, 0.5f, 0);

    private Renderer renderer;

    // ブロック上に存在するプレイヤーユニット
    private GameObject playerUnit;

    //private GameObject clone;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    /// <summary>
    /// オブジェクト選択中にマウスボタンが押されたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (BuildManager.instance.GetPlayerUnitToBuild() == null)
        {
            return;
        }

        // プレイヤーユニットが存在する場合
        if (playerUnit != null)
        {
            Debug.Log("ここには配置できません");
            return;
        }

        // 配置するプレイヤーユニットを取得
        GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
        playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
        // 連続で配置できないようにするためplayerUnitToBuildにNULLを入れる
        BuildManager.instance.playerUnitToBuild = null;
    }

    /// <summary>
    /// カーソルがオブジェクトに重なっているときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BuildManager.instance.GetPlayerUnitToBuild() == null)
        {
            return;
        }
        // Colorを変更する
        renderer.material.color = hoverColor;

        // 選択されているプレイヤーユニットを取得
        //GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
        //clone = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    }

    /// <summary>
    /// カーソルがオブジェクトから離れたときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Colorを初期Colorに戻す
        renderer.material.color = startColor;
        //Destroy(clone);
    }

    ///// <summary>
    ///// オブジェクト選択中にマウスボタンが押されたとき呼ばれる
    ///// </summary>
    //private void OnMouseDown()
    //{
    //    // プレイヤーユニットが存在する場合
    //    if(playerUnit != null) 
    //    {
    //        Debug.Log("ここには配置できません");
    //        return;
    //    }

    //    // 配置するプレイヤーユニットを取得
    //    GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
    //    playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    //}

    ///// <summary>
    ///// オブジェクトにマウスが重なっているときに呼ばれる
    ///// </summary>
    //private void OnMouseEnter()
    //{
    //    // Colorを変更する
    //    renderer.material.color = hoverColor;
    //}

    ///// <summary>
    ///// マウスがオブジェクトから離れたときに呼ばれる
    ///// </summary>
    //private void OnMouseExit()
    //{
    //    // Colorを初期Colorに戻す
    //    renderer.material.color = startColor;
    //}

}
