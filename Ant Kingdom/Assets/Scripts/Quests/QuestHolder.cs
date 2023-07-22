using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestHolder : MonoBehaviour
{
    public QuestData questData;
    public TextMeshProUGUI questName;
    public Transform rewardList;
    public TimerTooltip timerTooltip;
    public TextMeshProUGUI antLimitText;
    public TextMeshProUGUI dangerLevel;
    public Dictionary<AntManager.AntType, int> antSelected;
    public delegate void OnSelectedAntsChange();
    public event OnSelectedAntsChange onSelectedAntsChange;
    public GameObject questInfoPanelPrefab;
    public QuestPanel panel;
    public QuestButton questButton;
    public QuestInstance quest;
    public void Init(QuestData data, QuestPanel npanel) {
        questData = data;
        panel = npanel;
        questName.text = questData.questName;
        timerTooltip.Init();
        timerTooltip.InitTimer(TimeSpan.FromSeconds(questData.completionTime));
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            quest.FinishQuest();
        });

        antLimitText.text = "Min Ants: " + questData.minAnts.ToString() + "\n"
                          + "Max Ants: " + questData.maxAnts.ToString();

        dangerLevel.text = "Danger Level: " + questData.dangerLevel.ToString();
    
        foreach (QuestReward reward in questData.rewards) {
            Resource resource = GameResources.GetResourceFromType(reward.resourceType);
            GameObject go1 = new GameObject();
            GameObject go = Instantiate(go1, rewardList);
            Destroy(go1);
            go.AddComponent<RectTransform>();
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(45, 45);
            Image newImage = go.AddComponent<Image>();
            newImage.sprite = resource.GetIcon();
        }

        antSelected = new Dictionary<AntManager.AntType, int>();
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            antSelected[antType] = 0;
        }

        quest = new QuestInstance(questData, this);
        quest.onStateChanged += UpdateButton;
        UpdateButton();
    }

    public bool StartQuest() {
        return quest.StartQuest(questData, antSelected, timerTooltip);
    }

    public void CompleteQuest() {
        quest.CompleteQuest();
        timerTooltip.ResetTimer();
        panel.ShowRewards(quest);
    }

    public void UpdateButton() {
        questButton.UpdateButton(quest.state);
    }

    public int GetSelectedAntCount(AntManager.AntType type) {
        return antSelected[type];
    }

    public float GetTotalAntEfficiency() {
        float sum = 0;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            sum += antSelected[antType] * AntManager.GetAntData(antType).exploreEfficiency;
        }
        return sum;
    }

    public void AddAntSelection(AntManager.AntType type) {
        if (quest.state != QuestInstance.State.Inactive) return;
        antSelected[type]++;
        onSelectedAntsChange?.Invoke();
    }

    public void DecreaseAntSelection(AntManager.AntType type) {
        if (quest.state != QuestInstance.State.Inactive) return;
        antSelected[type]--;
        onSelectedAntsChange?.Invoke();
    }

    public void ResetSelection() {
        if (quest.state != QuestInstance.State.Inactive) return;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            antSelected[antType] = 0;
        }
        onSelectedAntsChange?.Invoke();
    }

    public void ShowInfo() {
        GameObject go = Instantiate(questInfoPanelPrefab, panel.transform);
        QuestInfoPanel qpanel = go.GetComponent<QuestInfoPanel>();
        qpanel.Init(this);
    }
}
