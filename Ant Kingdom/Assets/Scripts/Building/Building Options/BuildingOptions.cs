using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingOptions : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Building building;
    public GameObject buttonList;
    public TextMeshProUGUI buildingName;

    private bool inContext;

    private bool isClicking;
    private Vector3 clickPosition;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            isClicking = true;
            clickPosition = Input.mousePosition;
        }
        
        if (Input.GetMouseButtonUp(0)) {
            if (isClicking && clickPosition == Input.mousePosition && !inContext) {
                ClearBuildingSelection();
            }
            isClicking = false;
        }
    }
    public void SetBuilding(Building newBuilding) {
        building = newBuilding;
        buildingName.text = building.GetName() + " (Level " + (building.level + 1).ToString() + ")";
    }

    public void ClearBuildingSelection() {
        GridBuildingSystem.current.UnhighlightBuildingArea(building);
        Destroy(gameObject);
    }
    public void OnPointerExit(PointerEventData eventData) {
        inContext = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        inContext = true;
    }
}
