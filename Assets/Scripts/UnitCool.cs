using UnityEngine;

/// <summary>
/// �z�u���郆�j�b�g�̎擾���s��
/// </summary>
public class UnitCool : MonoBehaviour
{
    /// <summary>
    /// PlayerUnit1���擾
    /// </summary>
    public void PurchasePlayerUnit1()
    {
        Debug.Log("Get PlayerUnit_1");
        BuildManager.instance.SetPlayerUnit1ToBuild(BuildManager.instance.playerUnit1Prefab);
    }

    /// <summary>
    /// PlayerUnit2���擾
    /// </summary>
    public void PurchasePlayerUnit2()
    {
        Debug.Log("Get PlayerUnit_2");
        BuildManager.instance.SetPlayerUnit2ToBuild(BuildManager.instance.playerUnit2Prefab);
    }
}
