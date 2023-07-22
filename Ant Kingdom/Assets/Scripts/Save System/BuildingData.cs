using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingData
{
    public int level;
    public float[] position;

    public string buildingName;
    public string description;
    public int[] area;

    public BuildingData(Building building)
    {
        level = building.level;
        buildingName = building.states.buildingName;
        description = building.states.description;
        Vector3 buildingPos = building.transform.position;
        position = new float[]
        {
            buildingPos.x, buildingPos.y, buildingPos.z
        };
        Vector3Int areaPos = building.area.position;
        area = new int[]
        {
            areaPos.x, areaPos.y, areaPos.z
        };
    }
}
