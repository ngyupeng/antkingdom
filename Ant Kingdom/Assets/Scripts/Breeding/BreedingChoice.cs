using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreedingChoice : MonoBehaviour
{
    public AntData antData;
    public TextMeshProUGUI amountText;
    public Image antImage;

    private void Awake() {
        amountText.text = antData.foodCost.ToString();
        antImage.sprite = antData.sprite;
    }
}
