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
    public BreedingPanel breedingPanel;

    public void Init(AntData ant, BreedingPanel panel) {
        antData = ant;
        amountText.text = antData.foodCost.ToString();
        antImage.sprite = antData.sprite;
        breedingPanel = panel;
    }

    public void AddToQueue() {
        breedingPanel.AddAnt(antData);
    }
}
