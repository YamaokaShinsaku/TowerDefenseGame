using UnityEngine;

/// <summary>
/// 配置するユニットの取得を行う
/// </summary>
public class UnitCool : MonoBehaviour
{
    /// <summary>
    /// PlayerUnit1を取得
    /// </summary>
    public void PurchasePlayerUnit1()
    {
        Debug.Log("Get PlayerUnit_1");
        BuildManager.instance.SetPlayerUnit1ToBuild(BuildManager.instance.playerUnit1Prefab);
    }

    /// <summary>
    /// PlayerUnit2を取得
    /// </summary>
    public void PurchasePlayerUnit2()
    {
        Debug.Log("Get PlayerUnit_2");
        BuildManager.instance.SetPlayerUnit2ToBuild(BuildManager.instance.playerUnit2Prefab);
    }
}
