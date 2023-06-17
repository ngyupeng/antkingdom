using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopupController : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject floatingTextPrefab;
    private RectTransform rectTransform;
    private void Awake() {
        rectTransform = transform.GetComponent<RectTransform>();
        GameResources.onNotEnoughResources += ShowNotEnoughResources;
    }

    public void ShowFloatingText(string text, Color color) {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, uiCamera, out mousePosition);
        var go = Instantiate(floatingTextPrefab, transform);
        go.transform.localPosition = mousePosition;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Not Enough Resources!";
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void ShowNotEnoughResources() {
        ShowFloatingText("Not Enough Resources!", Color.red);
    }
}