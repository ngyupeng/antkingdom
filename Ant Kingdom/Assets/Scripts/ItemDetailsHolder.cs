using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsHolder : MonoBehaviour
{
    private ShopItem item;

    [SerializeField] private GameObject resourceList;
    [SerializeField] private GameObject resourceCostPrefab;

    private void Start() {
        SetHolderInactive();
    }

    public void InitialiseView(ShopItem Item) {
        item = Item;
        foreach (Transform child in resourceList.transform) {
            Destroy(child.gameObject);
        }
        foreach (var resourceCost in item.resourceCostsList) {
            GameObject itemObject = Instantiate(resourceCostPrefab, resourceList.transform);
            itemObject.GetComponent<ResourceCostHolder>().Initialise(resourceCost.resource, resourceCost.cost);
        }
        // Prevents weird display on opening
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    public void UpdateView() {

    }

    public void SetHolderActive() {
        gameObject.SetActive(true);
    }

    public void SetHolderInactive() {
        gameObject.SetActive(false);
    }
}
