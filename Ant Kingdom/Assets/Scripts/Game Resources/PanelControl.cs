using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelControl : MonoBehaviour
{
    private GameObject panel;
    private GameObject uiBlock;
    private Transform panelTransform;

    private void Awake()
    {
        uiBlock = transform.GetChild(0).gameObject;
        panelTransform = transform.GetChild(1);
        panel = panelTransform.gameObject;
        ResourceNode.onSelect += UpdateActive;
        GameResources.onResourceAmountChanged += UpdateView;
    }

    public void UpdateActive() {
        uiBlock.SetActive(true);
        panel.SetActive(true);
        UpdateView();
    }  

    public void UpdateInactive() {
        uiBlock.SetActive(false);
        panel.SetActive(false);
    }

    void UpdateView() {
        ResourceNode node = ResourceNode.selectedNode;
        panelTransform.Find("itemDesc").GetComponent<TextMeshProUGUI>().text = node.GetName();
        panelTransform.Find("nodeImage").GetComponent<Image>().sprite = node.GetSprite();
        panelTransform.Find("resourceIcon").GetComponent<Image>().sprite = node.GetResourceIcon();
        panelTransform.Find("amount").GetComponent<TextMeshProUGUI>().text = node.GetAmount().ToString();
    }

    private void OnDestroy() {
        ResourceNode.onSelect -= UpdateActive;
        GameResources.onResourceAmountChanged -= UpdateView;
    }
}
