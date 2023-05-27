using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopupController : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    private void Awake() {
        GameResources.onNotEnoughResources += ShowNotEnoughResources;
    }

    public void ShowFloatingText(string text, Color color) {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var go = Instantiate(floatingTextPrefab, mousePosition, Quaternion.identity, transform);
        go.GetComponent<TextMeshProUGUI>().text = "Not Enough Resources!";
        go.GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void ShowNotEnoughResources() {
        ShowFloatingText("Not Enough Resources!", Color.red);
    }
}
