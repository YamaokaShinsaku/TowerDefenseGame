using UnityEngine;

public enum Sequence
{
    SelectDirection,    // プレイヤーユニットの向きを選択
    BuildPlayerUnit,    // プレイヤーユニットを配置
    None
}

/// <summary>
/// ブロック上に何かが構築されているかを調べる
/// </summary>
public class Node : MonoBehaviour
{
    // 選択されたときのColor
    public Color hoverColor;
    // 初期のColorを保存する
    private Color startColor;

    private Vector3 builPositonOffset = new Vector3(0, 0.5f, 0);

    private Renderer renderer;

    Sequence sequence;

    // ブロック上に存在するプレイヤーユニット
    private GameObject playerUnit;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    /// <summary>
    /// オブジェクト選択中にマウスボタンが押されたとき呼ばれる
    /// </summary>
    private void OnMouseDown()
    {
        // プレイヤーユニットが存在する場合
        if(playerUnit != null) 
        {
            Debug.Log("ここには配置できません");
            return;
        }

        // 配置するプレイヤーユニットを取得
        GameObject playerUnitToBuild = BuildManager.instance.GetPlayerUnitToBuild();
        playerUnit = Instantiate(playerUnitToBuild, this.transform.position + builPositonOffset, this.transform.rotation);
    }

    /// <summary>
    /// オブジェクトにマウスが重なっているときに呼ばれる
    /// </summary>
    private void OnMouseEnter()
    {
        // Colorを変更する
        renderer.material.color = hoverColor;
    }

    /// <summary>
    /// マウスがオブジェクトから離れたときに呼ばれる
    /// </summary>
    private void OnMouseExit()
    {
        // Colorを初期Colorに戻す
        renderer.material.color = startColor;
    }
}
