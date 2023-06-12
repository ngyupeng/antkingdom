using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOptions : MonoBehaviour
{
    public Building building;

    public void SetBuilding(Building newBuilding) {
        building = newBuilding;
    }

    public void MoveBuilding() {
        building.MoveBuilding();
        Destroy(this.gameObject);
    }
}
