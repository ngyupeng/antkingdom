using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItemHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ShopItem item;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image iconImage;
    private ResourceCost[] resourceCostsList;
    private float buildTime;

    public delegate void OnSelect();
    public static event OnSelect onSelect;

    private ItemDetailsHolder detailsHolder;
    public void Initialise(ShopItem Item, ItemDetailsHolder holder) {
        item = Item;

        itemName.text = item.itemName;
        iconImage.sprite = item.icon;
        resourceCostsList = item.resourceCostsList;
        buildTime = item.buildTime;
        detailsHolder = holder;
    }

    public void OnPointerClick(PointerEventData data) {
        GridBuildingSystem.current.InitialiseWithBuilding(item.prefab);
        onSelect?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailsHolder.SetHolderActive();
        Debug.Log("Entered");
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        detailsHolder.SetHolderInactive();
        Debug.Log("Exited");
    }
}
