using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCapacityTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Resource resource;

    private void Awake() {
        GameResources.onResourceCapacityChanged += UpdateText;
    }
    public void Initialise(Resource nResource) {
        resource = nResource;
        text.text = "Max: " + GameResources.GetResourceCapacity(resource.GetResourceType()).ToString();
    }

    public void UpdateText() {
        text.text = "Max: " + GameResources.GetResourceCapacity(resource.GetResourceType()).ToString();
    }

    private void OnDestroy() {
        GameResources.onResourceCapacityChanged -= UpdateText;
    }
}
