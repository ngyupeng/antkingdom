using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    public ShopItem shopItem;
    public Vector3 originPosition;

    // This checks whether it has already been bought
    public bool bought { get; private set; }
    public bool placed { get; private set; }
    public BoundsInt area;

    private PolygonCollider2D col;

    public delegate void OnSelect();
    public static event OnSelect onSelect;

    void Awake() {
        col = gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = false;
    }
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

        if (!bought) {
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
        if (!bought) {
            if (!GameResources.UseResourceListAmounts(shopItem.resourceCostsList)) {
                return;
            }
        }

        bought = true;
        placed = true;
        col.enabled = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
        AstarPath.active.Scan();
    }

    public void MoveBuilding() {
        placed = false;
        col.enabled = false;
        GridBuildingSystem.current.ClearMainArea(area);
        GridBuildingSystem.current.SetBuilding(this);
    }

    #endregion
    
    private void OnMouseUp() {
        Debug.Log("Building Clicked");
        if (!placed) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        BuildingUIControl.selectedBuilding = this;
        onSelect?.Invoke();
    }
}
