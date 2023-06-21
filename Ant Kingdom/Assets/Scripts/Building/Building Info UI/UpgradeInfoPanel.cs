using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI upgradeTime;
    public Image buildingSprite;
    public GameObject statList;
    public GameObject resourceList;
    public TextMeshProUGUI description;
    public GameObject upgradeStatHolderPrefab;
    public GameObject upgradeCostHolderPrefab;
    public Building building;

    public void Initialise() {
        foreach (Transform child in statList.transform) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in resourceList.transform) {
            Destroy(child.gameObject);
        }

        Initialise(BuildingUIControl.selectedBuilding);
    }
    public void Initialise(Building nBuilding) {
        building = nBuilding;
        building.DisplayUpgradeInfo(this);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void UpgradeBuilding() {
        if (building.CanUpgrade()) {
            building.Upgrade();
            gameObject.SetActive(false);
        }
    }
}
