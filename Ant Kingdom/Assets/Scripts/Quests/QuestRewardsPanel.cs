using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardsPanel : MonoBehaviour
{
    public Transform antList;
    public Transform resourceList;
    public GameObject antCountPrefab;
    public GameObject resourceCountPrefab;
    public GameObject noneTextPrefab;
    public void Init(QuestInstance quest) {
        bool hasAnt = false;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            int count = quest.GetSurvivingAnts(antType);
            if (count <= 0) continue;
            hasAnt = true;
            GameObject go = Instantiate(antCountPrefab, antList);
            QuestResultAntCount ac = go.GetComponent<QuestResultAntCount>();
            ac.Init(antType, count);
        }
        if (!hasAnt) {
            Instantiate(noneTextPrefab, antList);
        }

        bool hasResources = false;
        foreach (QuestReward reward in quest.questData.rewards) {
            GameResources.ResourceType type = reward.resourceType;
            int count = quest.GetResourceObtained(type);
            if (count <= 0) continue;
            hasResources = true;
            GameObject go = Instantiate(resourceCountPrefab, resourceList);
            QuestResultResourceCount rc = go.GetComponent<QuestResultResourceCount>();
            rc.Init(type, count);
        }
        if (!hasResources) {
            Instantiate(noneTextPrefab, resourceList);
        }
    }
}
