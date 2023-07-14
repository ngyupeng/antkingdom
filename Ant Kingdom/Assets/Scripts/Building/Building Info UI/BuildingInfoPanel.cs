using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI buildingTitle;
    public Image buildingSprite;
    public GameObject statList;
    public TextMeshProUGUI description;
    public GameObject statHolderPrefab;
    public GameObject antCountList;
    public Building building;

    public void Initialise() {
        foreach (Transform child in statList.transform) {
            Destroy(child.gameObject);
        }
        Initialise(BuildingUIControl.selectedBuilding);
    }
    public void Initialise(Building nBuilding) {
        building = nBuilding;
        building.DisplayInfo(this);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
