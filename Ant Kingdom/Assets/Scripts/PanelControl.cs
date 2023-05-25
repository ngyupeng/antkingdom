using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelControl : MonoBehaviour
{
    private GameObject panel;
    private Transform panelTransform;

    private void Awake()
    {
        panelTransform = transform.GetChild(0);
        panel = panelTransform.gameObject;
        ResourceNode.onSelect += updateActive;
        GameResources.onResourceAmountChanged += updateActive;
    }

    public void updateActive() {
        panel.SetActive(true);
        updateView();
        Debug.Log("Updated");
    }  

    void updateView() {
        ResourceNode node = ResourceNode.selectedNode;
        panelTransform.Find("itemDesc").GetComponent<TextMeshProUGUI>().text = node.GetName();
        panelTransform.Find("nodeImage").GetComponent<Image>().sprite = node.GetSprite();
        panelTransform.Find("resourceIcon").GetComponent<Image>().sprite = node.GetResourceIcon();
        panelTransform.Find("amount").GetComponent<TextMeshProUGUI>().text = node.GetAmount().ToString();
    }
}
