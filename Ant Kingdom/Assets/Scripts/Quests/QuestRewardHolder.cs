using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRewardHolder : MonoBehaviour
{
    public Image resourceIcon;
    public TextMeshProUGUI resourceAmount;
    public TextMeshProUGUI chance;

    public void Init(QuestReward reward) {
        resourceIcon.sprite = GameResources.GetResourceFromType(reward.resourceType).GetIcon();
        resourceAmount.text = reward.minAmount.ToString() + " - " + reward.maxAmount.ToString();
        chance.text = "(" + (reward.chance * 100).ToString() +  "%)";
    }
}
