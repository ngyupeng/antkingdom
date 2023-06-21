using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// For testing only, delete/change
public class Window_GameResources : MonoBehaviour
{
    private void Start()
    {
        GameResources.onResourceAmountChanged += UpdateResourceText;
        UpdateResourceText();
        Debug.Log("Window Awake");
    }
    private void UpdateResourceText()
    {
        transform.Find("resourceAmount").GetComponent<TextMeshProUGUI>().text = 
            "STONE: " + GameResources.GetResourceAmount(GameResources.ResourceType.Stone) + "\n" +
            "WOOD: " + GameResources.GetResourceAmount(GameResources.ResourceType.Wood) + "\n";
    }

    private void OnDestroy() {
        GameResources.onResourceAmountChanged -= UpdateResourceText;
    }
}
