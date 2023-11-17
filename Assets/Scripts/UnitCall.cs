using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 配置するユニットの取得を行う
/// </summary>
public class UnitCall : MonoBehaviour
{
    // 各プレイヤーユニットのデータを設定
    //public PlayerUnitData unit1Data;
    //public PlayerUnitData unit2Data;
    public PlayerUnitData[] unitDatas;
    // 各プレイヤーユニットの使用コストを表示するUI
    public Text[] unitUseCostText;

    private void Start()
    {
        // 使用コストを表示
        for (int i = 0; i < unitDatas.Length; i++)
        {
            unitUseCostText[i].text = unitDatas[i].cost.ToString();
        }
    }

    /// <summary>
    /// 指定の番号のPlayerUnitを取得
    /// </summary>
    /// <param name="unitNumber"></param>
    public void SelectUnit(int unitNumber)
    {
        BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[unitNumber-1]);
    }
    ///// <summary>
    ///// PlayerUnit1を取得
    ///// </summary>
    //public void SelectPlayerUnit1()
    //{
    //    Debug.Log("Get PlayerUnit_1");
    //    //BuildManager.instance.SelectPlayerUnitToBuild(unit1Data);
    //    BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[0]);
    //}

    ///// <summary>
    ///// PlayerUnit2を取得
    ///// </summary>
    //public void SelectPlayerUnit2()
    //{
    //    Debug.Log("Get PlayerUnit_2");
    //    //BuildManager.instance.SelectPlayerUnitToBuild(unit2Data);
    //    BuildManager.instance.SelectPlayerUnitToBuild(unitDatas[1]);
    //}

}
