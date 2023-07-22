using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestResultAntCount : MonoBehaviour
{
    public Image ant;
    public TextMeshProUGUI antCount;

    public void Init(AntManager.AntType type, int count) {
        ant.sprite = AntManager.GetAntData(type).sprite;
        antCount.text = count.ToString() + "x";
    }
}
