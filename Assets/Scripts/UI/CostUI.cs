using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 現在のコストを表示
/// </summary>
public class CostUI : MonoBehaviour
{
    // 現在のコスト量を表示するText
    public Text costText;

    // Update is called once per frame
    void Update()
    {
        costText.text = PlayerStats.cost.ToString();
    }
}
