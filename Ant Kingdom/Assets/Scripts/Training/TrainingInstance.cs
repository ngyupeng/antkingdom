using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingInstance : MonoBehaviour
{
    public AntData antData { get; private set; }
    public Image antImage;
    public TimerTooltip timerTooltip;
    TrainingPanel panel;
    public void Init(AntData ant, TrainingPanel tpanel) {
        antData = ant;
        panel = tpanel;
        antImage.sprite = ant.sprite;
        timerTooltip.Init();
        timerTooltip.InitTimer(TimeSpan.FromSeconds(antData.trainingTime));
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            panel.FinishTraining(ant);
            Destroy(gameObject);
        });
        timerTooltip.StartTimer();
    }

    public void CancelTraining() {
        panel.CancelTraining();
        foreach (ResourceCost cost in antData.trainingResourceCosts) {
            GameResources.AddResourceAmount(cost.resource.GetResourceType(), cost.cost);
        }
        Destroy(gameObject);
    }
}
