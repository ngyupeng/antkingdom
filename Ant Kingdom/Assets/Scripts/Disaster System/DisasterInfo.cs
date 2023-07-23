using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DisasterInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public TextMeshProUGUI disasterLevel;
    public DisasterSystem system;

    private void Awake() {
        UpdateText();
    }

    public void UpdateText() {
        disasterLevel.text = DisasterSystem.minDamage.ToString() + " - " + DisasterSystem.maxDamage.ToString();
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        tooltip.SetActive(false);
    }
}
