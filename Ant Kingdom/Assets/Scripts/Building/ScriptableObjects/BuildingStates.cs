using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStates : ScriptableObject
{
    public string buildingName;
    public string description;
    public BuildingLevel[] levels;

    public virtual void Initialise() {

    }
}
