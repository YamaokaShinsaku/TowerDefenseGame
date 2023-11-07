using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残りライフを表示
/// </summary>
public class LifeUI : MonoBehaviour
{
    // ライフのテキスト
    public Text lifeText;

    // Update is called once per frame
    void Update()
    {
        lifeText.text = PlayerStats.life.ToString();
    }
}
