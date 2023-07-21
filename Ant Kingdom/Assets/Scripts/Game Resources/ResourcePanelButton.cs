using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePanelButton : MonoBehaviour
{
    public delegate void OnButtonClicked();
    public static event OnButtonClicked onButtonClicked;

    public void CollectResources() {
        if (!AntManager.HasIdleAnt()) return;
        AntManager.AntType type = AntManager.FindIdleAnt();
        AntManager.UseIdleAnts(type, 1);
        GameObject ant = AntSpawner.instance.SpawnAnt(type);
        CollectResource cr = ant.GetComponent<CollectResource>();
        cr.SetTargetNode(ResourceNode.selectedNode);
    }
}
