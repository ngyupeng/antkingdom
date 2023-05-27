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
    [SerializeField] private GameObject floatingTextPrefab;
    public void Initialise(ShopItem Item, ItemDetailsHolder holder) {
        item = Item;

        itemName.text = item.itemName;
        iconImage.sprite = item.icon;
        resourceCostsList = item.resourceCostsList;
        buildTime = item.buildTime;
        detailsHolder = holder;
    }

    public void OnPointerClick(PointerEventData data) {
        if (!GameResources.RequireResourceListAmounts(item.resourceCostsList)) {
            return;
        }
        GridBuildingSystem.current.InitialiseWithBuilding(item.prefab);
        GridBuildingSystem.current.SetShopItem(item);
        onSelect?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailsHolder.SetHolderActive();
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        detailsHolder.SetHolderInactive();
    }
    public void ShowNotEnoughResources() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition.x + " " + mousePosition.y + " " + mousePosition.z);
        var go = Instantiate(floatingTextPrefab, mousePosition, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = "Not Enough Resources!";
        go.GetComponent<TextMeshPro>().color = Color.red;
    }
}
