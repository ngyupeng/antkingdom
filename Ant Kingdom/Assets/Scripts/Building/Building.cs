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
        originPosition = transform.localPosition;
        
        // If item is from shop, it requires resources.
        if (!bought) {
            GameResources.UseResourceListAmounts(shopItem.resourceCostsList);
        }

        bought = true;
        placed = true;
        col.enabled = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
        AstarPath.active.Scan();
    }

    public void CancelPlacement() {
        if (!bought) {
            Destroy(gameObject);
        } else {
            placed = true;
            col.enabled = true;
            transform.localPosition = originPosition;
        }
    }

    public void MoveBuilding() {
        placed = false;
        col.enabled = false;
        GridBuildingSystem.current.ClearMainArea(area);
        GridBuildingSystem.current.SetBuilding(this);
    }

    #endregion
    
    private bool isClicking;
    private Vector3 clickPosition;
    private void OnMouseDown() {
        if (!placed) return;
        isClicking = true;
        clickPosition = Input.mousePosition;
    }
    // Should only do stuff if there is no dragging
    private void OnMouseUp() {
        if (!placed) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isClicking && clickPosition == Input.mousePosition) { 
            BuildingUIControl.selectedBuilding = this;
            onSelect?.Invoke();
        }
        isClicking = false;
    }
}
