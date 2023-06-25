using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Housing States", menuName = "GameObjects/HousingStates")]
public class HousingStates : BuildingStates
{
    public HousingLevel[] housingLevels { get; private set; }

    private bool initialised = false;

    public override void Initialise() {
        if (!initialised) {
            initialised = true;
            HousingLevel[] tempLevels = new HousingLevel[levels.Length];
            for (int i = 0; i < levels.Length; i++) {
                tempLevels[i] = levels[i] as HousingLevel;
            }
            housingLevels = tempLevels;
        }
    }
}
