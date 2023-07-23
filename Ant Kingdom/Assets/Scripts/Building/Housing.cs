using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing : Building
{
    public HousingStates housingStates { get; private set; }

    protected override void Awake() {
        base.Awake();
        housingStates = base.states as HousingStates;
    }

    public override void DisplayInfo(BuildingInfoPanel panel) {
        base.DisplayInfo(panel);
        panel.buildingSprite.sprite = housingStates.housingLevels[level].buildingSprite;
        DisplayStat(IconDatabase.GetIcon("Ant"), "Ant Capacity", housingStates.housingLevels[level].antCapacity, panel);
        DisplayStat(IconDatabase.GetIcon("Heart"), "Health", housingStates.housingLevels[level].health, panel);
        
        panel.antCountList.SetActive(true);
    }

    public override void DisplayUpgradeInfo(UpgradeInfoPanel panel) {
        base.DisplayUpgradeInfo(panel);
        panel.buildingSprite.sprite = housingStates.housingLevels[level].buildingSprite;
        DisplayUpgradeStat(IconDatabase.GetIcon("Ant"), "Ant Capacity", 
            housingStates.housingLevels[level].antCapacity, housingStates.housingLevels[level + 1].antCapacity, panel);
        DisplayUpgradeStat(IconDatabase.GetIcon("Heart"), "Health", 
            housingStates.housingLevels[level].health ,housingStates.housingLevels[level + 1].health, panel);
    }

    public override void FinishBuilding()
    {
        base.FinishBuilding();
        AntManager.AddAntCapacity(housingStates.housingLevels[0].antCapacity);
    }
    public override void FinishUpgrade()
    {
        base.FinishUpgrade();
        AntManager.AddAntCapacity(housingStates.housingLevels[level].antCapacity 
            - housingStates.housingLevels[level - 1].antCapacity);
    }

    public override void Destroyed()
    {
        base.Destroyed();
        if (bought) {
            AntManager.AddAntCapacity(-housingStates.housingLevels[level].antCapacity);
        }

        Destroy(gameObject);
    }
}
