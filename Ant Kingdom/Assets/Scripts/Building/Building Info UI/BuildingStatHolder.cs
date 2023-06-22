using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildingStatHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statAmount;

    public void Initialise(Sprite iconSprite, string name, int amount) {
        icon.sprite = iconSprite;
        statName.text = name;
        statAmount.text = amount.ToString();
    }
}
