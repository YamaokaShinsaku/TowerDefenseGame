using UnityEngine;

/// <summary>
/// 配置するユニットの取得を行う
/// </summary>
public class UnitCall : MonoBehaviour
{
    // 各プレイヤーユニットのデータを設定
    public PlayerUnitData unit1Data;
    public PlayerUnitData unit2Data;

    /// <summary>
    /// PlayerUnit1を取得
    /// </summary>
    public void SelectPlayerUnit1()
    {
        Debug.Log("Get PlayerUnit_1");
        BuildManager.instance.SelectPlayerUnitToBuild(unit1Data);
    }

    /// <summary>
    /// PlayerUnit2を取得
    /// </summary>
    public void SelectPlayerUnit2()
    {
        Debug.Log("Get PlayerUnit_2");
        BuildManager.instance.SelectPlayerUnitToBuild(unit2Data);
    }
}
