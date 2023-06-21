using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOptions : MonoBehaviour
{
    public Building building;
    public delegate void OnClickedInfo();
    public static event OnClickedInfo onClickedInfo;

    public void SetBuilding(Building newBuilding) {
        building = newBuilding;
    }

    public void MoveBuilding() {
        building.MoveBuilding();
        Destroy(this.gameObject);
    }

    public void ShowBuildingInfo() {
        onClickedInfo?.Invoke();
        Destroy(this.gameObject);
    }
}
