using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �c�胉�C�t��\��
/// </summary>
public class LifeUI : MonoBehaviour
{
    // ���C�t�̃e�L�X�g
    public Text lifeText;

    // Update is called once per frame
    void Update()
    {
        lifeText.text = PlayerStats.life.ToString();
    }
}
