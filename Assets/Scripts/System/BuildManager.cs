using UnityEngine;

/// <summary>
/// プレイヤーユニットの配置関連の処理を行う
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManagerのインスタンス
    public static BuildManager instance;

    // 配置するプレイヤーユニット
    //public GameObject playerUnitToBuild;
    private PlayerUnitData unitDataToBuild;
    private Node selectNode;
    public NodeUI nodeUI;

    // 各プレイヤーユニットのPrefab
    //public GameObject playerUnit1Prefab;
    //public GameObject playerUnit2Prefab;
    //public GameObject playerUnit3Prefab;

    // プレイヤーユニット生成時のエフェクト
    //public GameObject buildEffect;

    // プレイヤーユニットを設置できるかどうか
    public bool canBuildPlayerUnit { get { return unitDataToBuild != null; } }
    // コストが足りているかどうか
    public bool hasCost { get { return PlayerStats.cost >= unitDataToBuild.cost; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("BuildManagerがすでに存在しています");
            return;
        }
        instance = this;
    }

    /// <summary>
    /// 設置するプレイヤーユニットを選択
    /// </summary>
    /// <param name="playerunitDataToBuild">PlayerUnitData</param>
    public void SelectPlayerUnitToBuild(PlayerUnitData playerunitDataToBuild)
    {
        unitDataToBuild = playerunitDataToBuild;
        DeselectNode();
    }

    /// <summary>
    /// プレイヤーユニットの選択状態を解除する
    /// </summary>
    public void DeletePlayerUnitToBuild()
    {
        if (unitDataToBuild != null)
        {
            unitDataToBuild = null;
        }
    }

    /// <summary>
    /// 指定のNode上にプレイヤーユニットを設置
    /// </summary>
    /// <param name="node">Node</param>
    public void BuildPlayerUnitOnNode(Node node)
    {
        if (PlayerStats.cost < unitDataToBuild.cost)
        {
            Debug.Log("設置コストが足りません");
            return;
        }

        // コストを使用する
        PlayerStats.cost -= unitDataToBuild.cost;

        // 配置するプレイヤーユニットを取得
        GameObject playerUnit =
            Instantiate(unitDataToBuild.prefab, node.transform.position + node.builPositonOffset, node.transform.rotation);
        node.playerUnit = playerUnit;
        // 生成エフェクトの再生
        //Instantiate(buildEffect, node.transform.position + node.builPositonOffset, node.transform.rotation);

        // 連続で配置できないようにするためplayerUnitToBuildにNULLを入れる
        unitDataToBuild = null;

        Debug.Log("残りコスト　: " + PlayerStats.cost);
    }

    public void SelectNode(Node node)
    {
        if (selectNode == node)
        {
            DeselectNode();
            return;
        }

        selectNode = node;
        unitDataToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectNode = null;
        nodeUI.HideUI();
    }
}
