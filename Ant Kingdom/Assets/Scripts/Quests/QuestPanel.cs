using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public GameObject questHolderPrefab;
    public GameObject questRewardsPrefab;
    public Transform questList;
    private void Start() {
        // Clear first cos I probably have an instance in editor to see how it looks
        foreach (Transform child in questList) {
            Destroy(child.gameObject);
        }
        string questDataPath = @"Quests\Quest Data";
        QuestData[] all = Resources.LoadAll<QuestData>(questDataPath);
        foreach (var data in all) {
            GameObject go = Instantiate(questHolderPrefab, questList);
            QuestHolder questHolder = go.GetComponent<QuestHolder>();
            questHolder.Init(data, this);
        }
    }

    public void ShowRewards(QuestInstance quest) {
        GameObject go = Instantiate(questRewardsPrefab, transform);
        QuestRewardsPanel panel = go.GetComponent<QuestRewardsPanel>();
        panel.Init(quest);
    }
}
