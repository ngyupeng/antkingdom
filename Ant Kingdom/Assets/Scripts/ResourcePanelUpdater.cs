using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcePanelUpdater : MonoBehaviour
{
    public Sprite image;
    public TextMeshProUGUI itemDesc;
    public Sprite resourceType;
    public TextMeshProUGUI amount;
    private GameObject GO;

    public ResourcePanelUpdater() {
        
    }
    void Awake() {
        ResourceNode.onSelect += updateActive;
        Debug.Log("Test");
    }

    // Update is called once per frame
    void Update() {

    }

    public void updateActive() {
        gameObject.SetActive(true);
        updateView();
        Debug.Log("Updated");
    }  

    void updateView() {
        ResourceNode node = ResourceNode.selectedNode;
        transform.Find("nodeImage").GetComponent<Image>().sprite = node.getSprite();
        transform.Find("itemDesc").GetComponent<TextMeshProUGUI>().text = node.getName();
        // transform.Find("nodeImage").GetComponent<Image>().sprite = node.getSprite();
        transform.Find("amount").GetComponent<TextMeshProUGUI>().text = node.getAmount().ToString();
    }
}
