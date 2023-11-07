using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行管理を行う
/// </summary>
public class GameManager : MonoBehaviour
{
    // ゲームが終了したかどうか
    private bool isGameEnd = false;

    // Update is called once per frame
    void Update()
    {
        if(isGameEnd)
        {
            return;
        }

        // ライフが0になったとき
        if(PlayerStats.life <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    void GameOver()
    {
        isGameEnd = true;
        Debug.Log("Game Over");
    }
}
