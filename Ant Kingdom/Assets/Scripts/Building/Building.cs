using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public ShopItem shopItem;
    public Vector3 originPosition;

    // This checks whether it has already been placed once 
    // i.e. if it is a building directly from the shop or not
    public bool Placed { get; private set; }
    public BoundsInt area;
    void Start()
    {
        
    }

    #region Build Methods

    // This should only be called when player tries to place building,
    // otherwise the "not enough resources" text will popup.
    public bool CanBePlaced() {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (!Placed) {
            return GridBuildingSystem.current.CanTakeArea(areaTemp) 
                && GameResources.RequireResourceListAmounts(shopItem.resourceCostsList);
        } 

        return GridBuildingSystem.current.CanTakeArea(areaTemp);
    }

    public void Place() {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        originPosition = positionInt;
        
        // If item is from shop, it requires resources.
        if (!Placed) {
            if (!GameResources.UseResourceListAmounts(shopItem.resourceCostsList)) {
                return;
            }
        }

        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }

    #endregion
}
