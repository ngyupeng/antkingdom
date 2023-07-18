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

    public void Init(QuestData data) {
        questData = data;
        questName.text = questData.questName;
        timerTooltip.Init();
        timerTooltip.InitTimer(TimeSpan.FromSeconds(questData.completionTime));
        timerTooltip.timer.TimerFinishedEvent.AddListener(delegate
        {
            
        });

        antLimitText.text = "Min Ants: " + questData.minAnts.ToString() + "\n"
                          + "Max Ants: " + questData.maxAnts.ToString();
    
        foreach (QuestReward reward in questData.rewards) {
            Resource resource = GameResources.GetResourceFromType(reward.resourceType);
            GameObject go = Instantiate(new GameObject(), rewardList);
            go.AddComponent<RectTransform>();
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(45, 45);
            Image newImage = go.AddComponent<Image>();
            newImage.sprite = resource.GetIcon();
        }
    }
}
