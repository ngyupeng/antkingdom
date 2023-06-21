using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceCostHolder : MonoBehaviour
{
    private GameResources.ResourceType resourceHeld;
    private int resourceAmountInt;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceName;
    [SerializeField] private TextMeshProUGUI resourceAmount;

    private void Awake() {
         GameResources.onResourceAmountChanged += UpdateTextColor;
    }

    private void Start() {
        UpdateTextColor();
    }
    public void Initialise(Resource resource, int amount) {
        resourceHeld = resource.GetResourceType();
        resourceIcon.sprite = resource.GetIcon();
        resourceName.text = resource.GetName();
        resourceAmountInt = amount;
        resourceAmount.text = amount.ToString();
    }

    public void UpdateTextColor() {
        if (GameResources.HasResourceAmount(resourceHeld, resourceAmountInt)) {
            resourceAmount.color = Color.black;
            resourceName.color = Color.black;
        } else {
            resourceAmount.color = Color.red;
            resourceName.color = Color.red;
        }
    }

    private void OnDestroy () {
         GameResources.onResourceAmountChanged -= UpdateTextColor;
    }
}
