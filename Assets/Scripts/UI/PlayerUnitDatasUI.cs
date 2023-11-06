using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーユニットのデータUIの操作を行う
/// </summary>
public class PlayerUnitDatasUI : MonoBehaviour
{
    // 各プレイヤーユニットのデータUI
    public GameObject[] playerDatasUI;

    public static PlayerUnitDatasUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideAllPlayerUnitDatasUI();
        //for (int i = 0; i < playerDatasUI.Length; i++)
        //{
        //    playerDatasUI[i].SetActive(false);
        //}
    }

    /// <summary>
    /// 指定のプレイヤーユニットのデータUIを表示する
    /// </summary>
    /// <param name="index">表示するUIの番号</param>
    public void ShowPlayerUnitDatasUI(int index)
    {
        playerDatasUI[index].SetActive(true);
    }

    /// <summary>
    /// 指定のプレイヤーユニットのデータUIを非表示にする
    /// </summary>
    /// <param name="index">表示するUIの番号</param>
    public void HidePlayerUnitDatasUI(int index)
    {
        // 指定のUIが表示されている場合
        if (playerDatasUI[index].activeInHierarchy)
        {
            playerDatasUI[index].SetActive(false);
        }
    }

    /// <summary>
    /// すべてのプレイヤーユニットのデータUIを非表示にする
    /// </summary>
    public void HideAllPlayerUnitDatasUI()
    {
        for (int i = 0; i < playerDatasUI.Length; i++)
        {
            if (playerDatasUI[i].activeInHierarchy)
            {
                playerDatasUI[i].SetActive(false);
            }
            //playerDatasUI[i].SetActive(false);
        }
    }
}
