using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Building States", menuName = "GameObjects/StorageBuildingStates")]
public class StorageBuildingStates : ScriptableObject
{
    public StorageBuildingLevel[] levels;
}
