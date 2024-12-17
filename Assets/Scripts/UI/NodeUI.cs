using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject uiObject;

    public void SetTarget(Node node)
    {
        target = node;

        transform.position = target.GetBuildPosition();

        uiObject.SetActive(true);
    }

    public void HideUI()
    {
        uiObject.SetActive(false);
    }

    public void Return()
    {
        target.ReturnUnit();
        BuildManager.instance.DeselectNode();
    }
}
