using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRewardHolder : MonoBehaviour
{
    public QuestReward questReward;
    public Image resourceIcon;
    public TextMeshProUGUI resourceAmount;
    public TextMeshProUGUI chanceText;
    public float chance;
    public void Init(QuestReward reward) {
        questReward = reward;
        resourceIcon.sprite = GameResources.GetResourceFromType(reward.resourceType).GetIcon();
        resourceAmount.text = reward.minAmount.ToString() + " - " + reward.maxAmount.ToString();
    }

    public void UpdateChance(float totalEfficiency) {
        chance = questReward.ComputeChance(totalEfficiency);
        if (chance > 100f) chance = 100;
        chanceText.text = "(" + Mathf.Floor(chance).ToString() + "%)";
    }
}
