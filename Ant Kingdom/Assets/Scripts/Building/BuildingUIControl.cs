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
    private void Awake() {
        rectTransform = transform.GetComponent<RectTransform>();
        Building.onSelect += ShowBuildingOptions;
        BuildingOptions.onClickedInfo += ShowBuildingInfo;
    }

    private void Update() {
        if (buildingOptionsInstance != null) {
            Vector2 initPosition;
            Vector3 screenPoint = uiCamera.WorldToScreenPoint(selectedBuilding.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint,
                uiCamera, out initPosition);
            buildingOptionsInstance.transform.localPosition = initPosition;
        }
    }

    private void ShowBuildingOptions() {
        if (buildingOptionsInstance != null) {
            Destroy(buildingOptionsInstance);
        }
        Vector2 initPosition;
        Vector3 screenPoint = uiCamera.WorldToScreenPoint(selectedBuilding.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint,
            uiCamera, out initPosition);
        buildingOptionsInstance = Instantiate(buildingOptionsPrefab, transform);
        buildingOptionsInstance.transform.localPosition = initPosition;
        BuildingOptions options = buildingOptionsInstance.GetComponent<BuildingOptions>();
        options.SetBuilding(selectedBuilding);
    }

    public void ShowBuildingInfo() {
        buildingInfoPanel.SetActive(true);
        buildingInfoPanel.GetComponent<BuildingInfoPanel>().Initialise();
    }

    private void OnDestroy() {
        Building.onSelect -= ShowBuildingOptions;
        BuildingOptions.onClickedInfo -= ShowBuildingInfo;
    }
}
