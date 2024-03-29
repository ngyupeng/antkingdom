using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
    public Vector3 originPosition;
    public bool bought { get; protected set; }
    public bool placed { get; protected set; }
    public BoundsInt area;

    protected PolygonCollider2D col;

    public delegate void OnSelect();
    public static event OnSelect onSelect;

    public int level = 0;

    public BuildingStates states;

    public bool isBuilding = false;
    public TimerTooltip timerTooltip;

    public UnityEvent onStateChanged;
    private bool isAddedToDisaster = false;
    protected virtual void Awake() {
        col = gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = false;
        states.Initialise();
        onStateChanged = new UnityEvent();
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
                && GameResources.RequireResourceListAmounts(states.levels[0].resourceCostsList);
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
            StartBuilding();
        }

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
            Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
            area.position = positionInt;
            GridBuildingSystem.current.TakeArea(area);
        }
    }

    public void MoveBuilding() {
        placed = false;
        col.enabled = false;
        GridBuildingSystem.current.ClearMainArea(area);
        GridBuildingSystem.current.SetBuilding(this);
    }

    #endregion
    
    #region Building Info
    public virtual void DisplayInfo(BuildingInfoPanel panel) {
        panel.buildingTitle.text = states.buildingName + " (Level " + (level + 1).ToString() + ")";
        panel.description.text = states.description;
    }
        
    public void DisplayStat(Sprite iconSprite, string name, int amount, BuildingInfoPanel panel) {
        GameObject go = Instantiate(panel.statHolderPrefab, panel.statList.transform);
        go.GetComponent<BuildingStatHolder>().Initialise(iconSprite, name, amount);
    }

    #endregion

    #region Building Upgrade Info
    public virtual void DisplayUpgradeInfo(UpgradeInfoPanel panel) {
        panel.title.text = "Upgrade to Level " + (level + 2).ToString() + "";
        panel.description.text = states.description;
        panel.buildingName.text = states.buildingName;
        
        // Just in case, for testing purposes
        if (level + 1 >= states.levels.Length) return;

        panel.upgradeTime.text = states.levels[level + 1].buildTime.ToString() + "s";

        foreach (var resourceCost in states.levels[level + 1].resourceCostsList) {
            GameObject itemObject = Instantiate(panel.upgradeCostHolderPrefab, panel.resourceList.transform);
            itemObject.GetComponent<ResourceCostHolder>().Initialise(resourceCost.resource, resourceCost.cost);
        }
    }

    public void DisplayUpgradeStat(Sprite iconSprite, string name, int amount, int newAmount, UpgradeInfoPanel panel) {
        GameObject go = Instantiate(panel.upgradeStatHolderPrefab, panel.statList.transform);
        go.GetComponent<UpgradeStatHolder>().Initialise(iconSprite, name, amount, newAmount);
    }

    #endregion

    #region Cancel Building

    public virtual void CancelConstruction() {
        if (!isBuilding) return;
        if (!bought) {
            CancelBuilding();
        } else {
            CancelUpgrade();
        }
        isBuilding = false;
    }

    public virtual void CancelBuilding() {
        GameResources.GetResourceListAmounts(states.levels[0].resourceCostsList);
        GridBuildingSystem.current.ClearMainArea(area);
        Destroy(timerTooltip.gameObject);
        Destroy(gameObject);
    }

    public virtual void CancelUpgrade() {
        GameResources.GetResourceListAmounts(states.levels[level + 1].resourceCostsList);
        Destroy(timerTooltip.gameObject);
    }

    #endregion
    public virtual void DisplayOptions(BuildingUIControl control) {
        if (isBuilding) {
            control.AddOptionButton(control.buildingCancelButtonPrefab);
            control.AddOptionButton(control.buildingInfoButtonPrefab);
            return;
        }

        control.AddOptionButton(control.buildingMoveButtonPrefab);
        control.AddOptionButton(control.buildingInfoButtonPrefab);
        if (!IsMaxLevel()) {
            control.AddOptionButton(control.buildingUpgradeButtonPrefab);
        }
    }

    #region Value Functions
    public string GetName() {
        return states.buildingName;
    }
    public bool IsMaxLevel() {
        return level + 1 == states.levels.Length;
    }
    public bool CanUpgrade() {
        return !IsMaxLevel() && GameResources.RequireResourceListAmounts(states.levels[level + 1].resourceCostsList);
    }

    #endregion

    public void AddToDisaster() {
        if (!isAddedToDisaster) {
            DisasterSystem.buildings.Add(this);
            isAddedToDisaster = true;
        }
    }
    public virtual void StartBuilding() {
        AddToDisaster();
        isBuilding = true;
        GameResources.UseResourceListAmounts(states.levels[0].resourceCostsList);
        timerTooltip = BuildingUIControl.current.CreateTimer(this);
        timerTooltip.InitTimer(TimeSpan.FromSeconds(states.levels[0].buildTime));
        timerTooltip.timer.StartTimer();
        SaveSystem.inProgress.Add(this);
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            FinishBuilding();
            SaveSystem.inProgress.Remove(this);
            Destroy(timerTooltip.gameObject);
        });
    }

    public void RefundBuilding() { 
        if (!isBuilding) return;
        int targetLevel = level;
        if (bought) targetLevel++;
        foreach (ResourceCost cost in states.levels[targetLevel].resourceCostsList) {
            Resource resource = cost.resource;
            GameResources.AddResourceAmount(resource.GetResourceType(), cost.cost);
        }
    }

    public virtual void FinishBuilding() {
        AddToDisaster();
        isBuilding = false;
        bought = true;
        onStateChanged.Invoke();
        SaveSystem.buildings.Add(this);
    }

    public virtual void StartUpgrade() {
        isBuilding = true;
        GameResources.UseResourceListAmounts(states.levels[level + 1].resourceCostsList);
        timerTooltip = BuildingUIControl.current.CreateTimer(this);
        timerTooltip.InitTimer(TimeSpan.FromSeconds(states.levels[level + 1].buildTime));
        timerTooltip.timer.StartTimer();
        SaveSystem.inProgress.Add(this);
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            FinishUpgrade();
            SaveSystem.inProgress.Remove(this);
            Destroy(timerTooltip.gameObject);
        });
    }

    public virtual void FinishUpgrade() {
        isBuilding = false;
        level++;
        onStateChanged.Invoke();
    }

    public virtual void ReceiveDamage(float damage) {
        if (damage >= states.levels[level].health) {
            Destroyed();
        }
    }

    public virtual void Destroyed() {
        SaveSystem.buildings.Remove(this);
        DisasterSystem.buildingsToRemove.Add(this);
        if (isBuilding) {
            SaveSystem.inProgress.Remove(this);
        }
        GridBuildingSystem.current.ClearMainArea(area);
        if (isBuilding) {
            Destroy(timerTooltip.gameObject);
        }
    }

    #region Clicking
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

    #endregion
}
