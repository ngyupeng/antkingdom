using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingStatHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statAmount;

    public void Initialise(string name, string amount) {
        statName.text = name;
        statAmount.text = amount;
    }
}
