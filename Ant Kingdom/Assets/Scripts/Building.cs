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

    public bool CanBePlaced() {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (!Placed) {
            return GridBuildingSystem.current.CanTakeArea(areaTemp) 
                && GameResources.HasResourceListAmounts(shopItem.resourceCostsList);
        } 

        return GridBuildingSystem.current.CanTakeArea(areaTemp);
    }

    public void Place() {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        originPosition = positionInt;
        
        if (!Placed) {
            GameResources.UseResourceListAmounts(shopItem.resourceCostsList);
        }
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }

    #endregion
}
