using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItemHolder : MonoBehaviour, IPointerClickHandler
{
    private ShopItem item;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image iconImage;
    private ResourceCost[] resourceCostsList;
    private float buildTime;

    public delegate void OnSelect();
    public static event OnSelect onSelect;
    public void Initialise(ShopItem Item) {
        item = Item;

        itemName.text = item.itemName;
        iconImage.sprite = item.icon;
        resourceCostsList = item.resourceCostsList;
        buildTime = item.buildTime;
    }

    public void OnPointerClick(PointerEventData data) {
        GridBuildingSystem.current.InitialiseWithBuilding(item.prefab);
        onSelect?.Invoke();
    }
}
