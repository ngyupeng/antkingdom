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
    public Building building;

    public void Initialise() {
        Initialise(BuildingUIControl.selectedBuilding);
    }
    public void Initialise(Building nBuilding) {
        building = nBuilding;
        building.DisplayInfo(this);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
