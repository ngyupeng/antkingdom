using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private GameObject shopHolder;
    private Dictionary<BuildingType, List<ShopItem>> shopItems = new Dictionary<BuildingType, List<ShopItem>>(2);
    [SerializeField]
    private TabGroup shopTabs;
    [SerializeField]
    private GameObject itemPrefab;

    #region Unity Methods
    private void Awake() {
        shopHolder = transform.Find("Shop Holder").gameObject;
        ShopItemHolder.onSelect += SetShopInactive;

        Load();
        Initialise();
    }

    private void Start() {
        SetShopInactive();
    }

    #endregion

    #region Initialisation Methods 

    private void Load() {
        ShopItem[] items = Resources.LoadAll<ShopItem>("Shop Items");
        shopItems.Add(BuildingType.ResourceBuilding, new List<ShopItem>());
        shopItems.Add(BuildingType.Housing, new List<ShopItem>());

        int count = 0;
        foreach (var item in items) {
            shopItems[item.type].Add(item);
            count++;
        }

        Debug.Log(count);
    }

    private void Initialise() {
        for (int i = 0; i < shopItems.Keys.Count; i++) {
            BuildingType key = shopItems.Keys.ToArray()[i];
            foreach (var item in shopItems[(BuildingType) i]) {
                GameObject itemObject = Instantiate(itemPrefab, shopTabs.objectsToSwap[i].transform);
                itemObject.GetComponent<ShopItemHolder>().Initialise(item);
            }
        }
    }

    #endregion

    #region UI Control Methods
    public void ShopButtonClick() {
        SetShopActive();
    }

    public void OutsideClick() {
        SetShopInactive();
    }

    public void SetShopActive() {
        shopHolder.SetActive(true);
        Debug.Log("Shop Set Active");
    }

    public void SetShopInactive() {
        shopHolder.SetActive(false);
        Debug.Log("Shop Set Inacative");
    }

    #endregion
}
