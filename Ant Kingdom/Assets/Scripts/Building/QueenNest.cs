using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class QueenNest : Building
{
    protected override void Awake() {
        col = gameObject.GetComponent<PolygonCollider2D>();
        bought = true;
        placed = true;
        originPosition = transform.localPosition;
        states.Initialise();
        onStateChanged = new UnityEvent();
    }
    private void Start() {
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(transform.position);
        GridBuildingSystem.current.TakeArea(area);
    }
    public override void DisplayInfo(BuildingInfoPanel panel) {
        base.DisplayInfo(panel);
        panel.buildingSprite.sprite = states.levels[level].buildingSprite;
        DisplayStat(IconDatabase.GetIcon("Heart"), "Health", states.levels[level].health, panel);
    }

    public override void DisplayOptions(BuildingUIControl control)
    {
        base.DisplayOptions(control);
        if (isBuilding) return;
        control.AddOptionButton(control.buildingBreedButtonPrefab);
    }

    public override void Destroyed()
    {
        

    }
}
