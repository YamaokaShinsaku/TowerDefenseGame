using UnityEngine;

/// <summary>
/// プレイヤーユニットの配置関連の処理を行う
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManagerのインスタンス
    public static BuildManager instance;

    // 配置するプレイヤーユニット
    public GameObject playerUnitToBuild;

    // プレイヤーユニット
    public GameObject playerUnit1Prefab;
    public GameObject playerUnit2Prefab;

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
    /// 配置するプレイヤーユニットを取得する
    /// </summary>
    /// <returns>プレイヤーユニット</returns>
    public GameObject GetPlayerUnitToBuild()
    { 
        return playerUnitToBuild; 
    }

    /// <summary>
    /// PlayerUnit_1を取得
    /// </summary>
    /// <param name="playerUnit"></param>
    public void SetPlayerUnit1ToBuild(GameObject playerUnit)
    {
        playerUnitToBuild = playerUnit;
    }

    /// <summary>
    /// PlayerUnit_2を取得
    /// </summary>
    /// <param name="playerUnit"></param>
    public void SetPlayerUnit2ToBuild(GameObject playerUnit)
    {
        playerUnitToBuild = playerUnit;
    }
}
