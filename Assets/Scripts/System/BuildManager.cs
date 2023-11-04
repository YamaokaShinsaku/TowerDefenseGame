using UnityEngine;

/// <summary>
/// プレイヤーユニットの配置関連の処理を行う
/// </summary>
public class BuildManager : MonoBehaviour
{
    // BuildManagerのインスタンス
    public static BuildManager instance;

    // 配置するプレイヤーユニット
    private GameObject playerUnitToBuild;

    // デフォルトのプレイヤーユニット
    public GameObject standardPlayerUnitPrefab;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("BuildManagerがすでに存在しています");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        playerUnitToBuild = standardPlayerUnitPrefab;
    }

    /// <summary>
    /// 配置するプレイヤーユニットを取得する
    /// </summary>
    /// <returns>プレイヤーユニット</returns>
    public GameObject GetPlayerUnitToBuild()
    { 
        return playerUnitToBuild; 
    }
}
