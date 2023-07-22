using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceCountUI : MonoBehaviour
{
    public Resource resource;
    private int currentAmount;
    public TextMeshProUGUI resourceAmount;
    public Image resourceIcon;
    private void Awake() {
        resourceIcon.sprite = resource.GetIcon();
        GameResources.onResourceAmountChanged += UpdateCount;
    }

    private void Start() {
        currentAmount = GameResources.GetResourceAmount(resource.GetResourceType());
        resourceAmount.text = currentAmount.ToString();
    }

    public void UpdateCount() {
        currentAmount = GameResources.GetResourceAmount(resource.GetResourceType());
        resourceAmount.text = currentAmount.ToString();
    }

    private void OnDestroy() {
        GameResources.onResourceAmountChanged -= UpdateCount;
    }
}
