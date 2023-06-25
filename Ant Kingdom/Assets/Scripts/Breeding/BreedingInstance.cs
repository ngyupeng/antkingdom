using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreedingInstance : MonoBehaviour
{
    public int number;
    public AntData antData { get; private set; }
    public TextMeshProUGUI antNumberText;
    public Image antImage;
    public TimerTooltip timerTooltip;
    private bool isAddQueued = false;
    public void Initialise(AntData nAntData) {
        antData = nAntData;
        antImage.sprite = antData.sprite;
        number = 1;
        timerTooltip.Init();
        timerTooltip.InitTimer(TimeSpan.FromSeconds(antData.breedingTime));
        timerTooltip.timer.StartTimer();
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            if (AntManager.CanAddAnts(1)) {
                AddAnt();
                timerTooltip.RestartTimer();
            } else {
                AntManager.onAntNumberChanged += TryAddAnt;
                isAddQueued = true;
            }
        });
    }

    public void TryAddAnt() {
        if (AntManager.CanAddAnts(1)) {
            AntManager.onAntNumberChanged -= TryAddAnt;
            AddAnt();
            timerTooltip.RestartTimer();
            isAddQueued = false;
        }
    }
    public void AddAnt() {
        AntManager.AddAntAmount(antData.antType, 1);
        DecreaseBreeding();
    }

    public void CancelBreeding() {
        GameResources.AddResourceAmount(GameResources.ResourceType.Food, antData.foodCost);
        DecreaseBreeding();
    }
    public void DecreaseBreeding() {
        number--;
        UpdateAmountText();
        if (number == 0) {
            Destroy(gameObject);
        }
    }

    public void IncreaseBreeding() {
        number++;
        UpdateAmountText();
    }

    public void UpdateAmountText() {
        antNumberText.text = number.ToString() + "x";
    }

    private void OnDestroy() {
        if (isAddQueued) {
            AntManager.onAntNumberChanged -= TryAddAnt;
        }
    }
}
