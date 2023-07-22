using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Storage Building States", menuName = "GameObjects/StorageBuildingStates")]
public class StorageBuildingStates : BuildingStates
{
    public Resource storedResource;
    public StorageBuildingLevel[] storageLevels { get; private set; }
    private bool initialised = false;

    public override void Initialise() {
        if (!initialised) {
            initialised = true;
            StorageBuildingLevel[] tempLevels = new StorageBuildingLevel[levels.Length];
            for (int i = 0; i < levels.Length; i++) {
                tempLevels[i] = levels[i] as StorageBuildingLevel;
            }
            storageLevels = tempLevels;
        }
    }
}
