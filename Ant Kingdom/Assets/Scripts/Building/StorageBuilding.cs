using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : Building
{
    public StorageBuildingStates storageStates { get; private set; }

    protected new void Awake() {
        base.Awake();
        storageStates = base.states as StorageBuildingStates;
    }

    public override void DisplayInfo(BuildingInfoPanel panel) {
        base.DisplayInfo(panel);
        Debug.Log("Clicked Info");
        panel.buildingSprite.sprite = storageStates.levels[level].buildingSprite;
        DisplayStat(storageStates.storedResource.GetIcon(), "Storage Capacity", storageStates.levels[level].storageAmount, panel);
        DisplayStat(storageStates.storedResource.GetIcon(), "Health", storageStates.levels[level].health, panel);
    }
}
