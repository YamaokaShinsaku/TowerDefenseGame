using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ブロック上に何かが構築されているかを調べる
/// </summary>
public class Node : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    // 選択されたときのColor
    public Color hoverColor;

    // 必要なコストが足りないときのColor
    public Color notEnoughCostcolor;
    // 初期のColorを保存する
    private Color startColor;
    // 設置する際のオフセット座標
    public Vector3 builPositonOffset = new Vector3(0, 0.5f, 0);
    // ブロック上に存在するプレイヤーユニット
    public GameObject playerUnit;

    private Renderer renderer;

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
        // プレイヤーユニットが存在する場合
        if (playerUnit != null)
        {
            //Debug.Log("ここには配置できません");
            BuildManager.instance.SelectNode(this);
            return;
        }
        // プレイヤーユニットが設置できない場合
        if (!BuildManager.instance.canBuildPlayerUnit)
        {
            return;
        }

        // プレイヤーユニットを設置する
        BuildManager.instance.BuildPlayerUnitOnNode(this);

        PlayerUnitDatasUI.instance.HideAllPlayerUnitDatasUI();
    }

    /// <summary>
    /// カーソルがオブジェクトに重なっているときに呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // プレイヤーユニットが設置できない場合
        if (!BuildManager.instance.canBuildPlayerUnit)
        {
            return;
        }

        if (BuildManager.instance.hasCost)
        {
            // Colorを変更する
            renderer.material.color = hoverColor;
        }
        else
        {
            // Colorを変更する
            renderer.material.color = notEnoughCostcolor;
        }

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

    /// <summary>
    /// 設置する座標を取得
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBuildPosition()
    {
        return transform.position + builPositonOffset;
    }

    public void ReturnUnit()
    {
        Destroy(playerUnit);
    }

    /// <summary>
    /// オブジェクト選択中にマウスボタンが押されたとき呼ばれる
    /// </summary>
    //private void OnMouseDown()
    //{
    //    // プレイヤーユニットが存在する場合
    //    if (playerUnit != null)
    //    {
    //        Debug.Log("ここには配置できません");
    //        return;
    //    }

    //    //// 配置するプレイヤーユニットを取得
    //    //GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
    //    //playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
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
