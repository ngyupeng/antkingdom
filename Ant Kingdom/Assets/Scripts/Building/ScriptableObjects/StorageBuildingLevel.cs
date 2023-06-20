using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Storage Building Level", menuName = "GameObjects/StorageBuildingLevel")]
public class StorageBuildingLevel : BuildingLevel
{
    public Resource storedResource;
    public int storageAmount;
}
