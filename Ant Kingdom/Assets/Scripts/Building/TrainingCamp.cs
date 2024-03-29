using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainingCamp : Building
{
    protected override void Awake() {
        base.Awake();
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
        control.AddOptionButton(control.buildingTrainButtonPrefab);
    }
}
