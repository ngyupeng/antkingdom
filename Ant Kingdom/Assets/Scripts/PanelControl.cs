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
    }

    public void updateActive() {
        panel.SetActive(true);
        updateView();
        Debug.Log("Updated");
    }  

    void updateView() {
        ResourceNode node = ResourceNode.selectedNode;
        panelTransform.Find("itemDesc").GetComponent<TextMeshProUGUI>().text = node.getName();
        panelTransform.Find("nodeImage").GetComponent<Image>().sprite = node.getSprite();
        panelTransform.Find("resourceIcon").GetComponent<Image>().sprite = node.getResourceIcon();
        panelTransform.Find("amount").GetComponent<TextMeshProUGUI>().text = node.getAmount().ToString();
    }
}
