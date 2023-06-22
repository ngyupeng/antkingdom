using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeStatHolder : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statAmount;
    [SerializeField] private TextMeshProUGUI newStatAmount;

    public void Initialise(Sprite iconSprite, string name, int amount, int newAmount) {
        icon.sprite = iconSprite;
        statName.text = name;
        statAmount.text = amount.ToString();
        newStatAmount.text = newAmount.ToString();
    }
}
