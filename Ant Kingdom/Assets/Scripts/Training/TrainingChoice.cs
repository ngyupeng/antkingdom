using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingChoice : MonoBehaviour
{
    public AntData ant;
    public Image antImage;
    public TrainingPanel panel;

    public void Init(AntData data, TrainingPanel tpanel) {
        ant = data;
        panel = tpanel;
        antImage.sprite = ant.sprite;
    }

    public void DisplayInfo() {
        panel.DisplayAntInfo(ant);
    }
}
