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
        panel.buildingSprite.sprite = storageStates.storageLevels[level].buildingSprite;
        DisplayStat(storageStates.storedResource.GetIcon(), "Storage Capacity", storageStates.storageLevels[level].storageAmount, panel);
        DisplayStat(storageStates.storedResource.GetIcon(), "Health", storageStates.storageLevels[level].health, panel);
    }

    public override void DisplayUpgradeInfo(UpgradeInfoPanel panel) {
        base.DisplayUpgradeInfo(panel);
        panel.buildingSprite.sprite = storageStates.storageLevels[level].buildingSprite;
        DisplayUpgradeStat(storageStates.storedResource.GetIcon(), "Storage Capacity", 
            storageStates.storageLevels[level].storageAmount, storageStates.storageLevels[level + 1].storageAmount, panel);
        DisplayUpgradeStat(storageStates.storedResource.GetIcon(), "Health", 
            storageStates.storageLevels[level].health ,storageStates.storageLevels[level + 1].health, panel);
    }
}
