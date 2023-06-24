using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIControl : MonoBehaviour
{
    public static BuildingUIControl current;
    public static Building selectedBuilding;
    [SerializeField]
    private GameObject buildingOptionsPrefab;
    // There should only be one of this
    private GameObject buildingOptionsInstance;
    private RectTransform rectTransform;
    [SerializeField]
    private Camera uiCamera;

    [SerializeField] 
    private GameObject buildingInfoPanel;
    [SerializeField] 
    private GameObject upgradeInfoPanel;
    public GameObject buildingMoveButtonPrefab;
    public GameObject buildingInfoButtonPrefab;
    public GameObject buildingUpgradeButtonPrefab;
    public GameObject buildingCancelButtonPrefab;
    public GameObject timerTooltipPrefab;
    private void Awake() {
        current = this;
        rectTransform = transform.GetComponent<RectTransform>();
        Building.onSelect += ShowBuildingOptions;
        BuildingMoveButton.onClickedMove += MoveBuilding;
        BuildingInfoButton.onClickedInfo += ShowBuildingInfo;
        BuildingUpgradeButton.onClickedUpgrade += ShowBuildingUpgradeInfo;
        BuildingCancelButton.onClickedCancel += CancelBuilding;
    }

    private void Update() {
  
    }

    public TimerTooltip CreateTimer(Building building) {
        GameObject go = Instantiate(timerTooltipPrefab, transform);
        go.transform.SetAsFirstSibling();
        TimerTooltip tooltip = go.GetComponent<TimerTooltip>();
        tooltip.Initialise(building.gameObject, rectTransform, uiCamera);
        return tooltip;
    }

    private void MoveBuilding() {
        selectedBuilding.MoveBuilding();
        ClearBuildingSelection();
    }

    private void ClearBuildingSelection() {
        if (buildingOptionsInstance != null) {
            BuildingOptions options = buildingOptionsInstance.GetComponent<BuildingOptions>();
            options.ClearBuildingSelection();
        }
    }

    private void ShowBuildingOptions() {
        ClearBuildingSelection();
        buildingOptionsInstance = Instantiate(buildingOptionsPrefab, transform);
        BuildingOptions options = buildingOptionsInstance.GetComponent<BuildingOptions>();
        options.SetBuilding(selectedBuilding);
        GridBuildingSystem.current.HighlightBuildingArea(selectedBuilding);
        selectedBuilding.DisplayOptions(this);
    }

    public void AddOptionButton(GameObject prefab) {
        if (buildingOptionsInstance != null) {
            BuildingOptions options = buildingOptionsInstance.GetComponent<BuildingOptions>();
            Instantiate(prefab, options.buttonList.transform);
        }
    }

    public void ShowBuildingInfo() {
        buildingInfoPanel.SetActive(true);
        buildingInfoPanel.GetComponent<BuildingInfoPanel>().Initialise();
        ClearBuildingSelection();
    }

    public void ShowBuildingUpgradeInfo() {
        upgradeInfoPanel.SetActive(true);
        upgradeInfoPanel.GetComponent<UpgradeInfoPanel>().Initialise();
        ClearBuildingSelection();
    }

    public void CancelBuilding() {
        selectedBuilding.CancelConstruction();
        ClearBuildingSelection();
    }

    private void OnDestroy() {
        Building.onSelect -= ShowBuildingOptions;
        BuildingMoveButton.onClickedMove -= MoveBuilding;
        BuildingInfoButton.onClickedInfo -= ShowBuildingInfo;
        BuildingUpgradeButton.onClickedUpgrade -= ShowBuildingUpgradeInfo;
        BuildingCancelButton.onClickedCancel -= CancelBuilding;
    }
}
