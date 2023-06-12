using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIControl : MonoBehaviour
{
    public static Building selectedBuilding;
    [SerializeField]
    private GameObject buildingOptionsPrefab;
    private RectTransform rectTransform;
    [SerializeField]
    private Camera uiCamera;

    void Awake() {
        rectTransform = transform.GetComponent<RectTransform>();
        Building.onSelect += ShowBuildingOptions;
    }
    private void ShowBuildingOptions() {
        Vector2 initPosition;
        Vector3 screenPoint = uiCamera.WorldToScreenPoint(selectedBuilding.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint,
            uiCamera, out initPosition);
        var go = Instantiate(buildingOptionsPrefab, transform);
        go.transform.localPosition = initPosition;
        BuildingOptions options = go.GetComponent<BuildingOptions>();
        options.SetBuilding(selectedBuilding);
    }
}
