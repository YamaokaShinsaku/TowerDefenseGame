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

    // 各プレイヤーユニットのPrefab
    public GameObject playerUnit1Prefab;
    public GameObject playerUnit2Prefab;

    // プレイヤーユニットを設置できるかどうか
    public bool CanBuildPlayerUnit { get { return unitDataToBuild != null; } }

    private void Awake()
    {
        if(instance != null)
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
    }

    /// <summary>
    /// 指定のNode上にプレイヤーユニットを設置
    /// </summary>
    /// <param name="node">Node</param>
    public void BuildPlayerUnitOnNode(Node node)
    {
        // 配置するプレイヤーユニットを取得
        GameObject playerUnit =
            Instantiate(unitDataToBuild.prefab, node.transform.position + node.builPositonOffset, node.transform.rotation);
        node.playerUnit = playerUnit;

        // 連続で配置できないようにするためplayerUnitToBuildにNULLを入れる
        unitDataToBuild = null;
    }
}
