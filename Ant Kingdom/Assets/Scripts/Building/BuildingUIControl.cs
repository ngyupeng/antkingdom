using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIControl : MonoBehaviour
{
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
    private void Awake() {
        rectTransform = transform.GetComponent<RectTransform>();
        Building.onSelect += ShowBuildingOptions;
        BuildingMoveButton.onClickedMove += MoveBuilding;
        BuildingInfoButton.onClickedInfo += ShowBuildingInfo;
        BuildingUpgradeButton.onClickedUpgrade += ShowBuildingUpgradeInfo;
    }

    private void Update() {
  
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

    private void OnDestroy() {
        Building.onSelect -= ShowBuildingOptions;
        BuildingMoveButton.onClickedMove -= MoveBuilding;
        BuildingInfoButton.onClickedInfo -= ShowBuildingInfo;
        BuildingUpgradeButton.onClickedUpgrade -= ShowBuildingUpgradeInfo;
    }
}
