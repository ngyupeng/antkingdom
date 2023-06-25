using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeButton : MonoBehaviour
{
    public delegate void OnClickedUpgrade();
    public static event OnClickedUpgrade onClickedUpgrade;

    public void ShowBuildingUpgradeInfo() {
        onClickedUpgrade?.Invoke();
    }
}
