using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoPanel : MonoBehaviour
{
    public QuestHolder holder { get; private set; }
    public QuestSelectedAntList selectedList;
    public QuestAntCountList availableList;
    public Transform rewardList;
    public GameObject rewardHolderPrefab;
    public QuestButton questButton;
    public void Init(QuestHolder nHolder) {
        holder = nHolder;
        holder.onSelectedAntsChange += UpdateSelectedList;
        foreach (QuestReward reward in holder.questData.rewards) {
            GameObject go = Instantiate(rewardHolderPrefab, rewardList);
            QuestRewardHolder rewardHolder = go.GetComponent<QuestRewardHolder>();
            rewardHolder.Init(reward);
        }
        UpdateButton();
        holder.quest.onStateChanged += UpdateButton;
    }

    public void StartQuest() {
        bool success = holder.StartQuest();
        if (success) {
            gameObject.SetActive(false);
        }
    }

    public void CompleteQuest() {
        holder.CompleteQuest();
    }

    public void UpdateButton() {
        questButton.UpdateButton(holder.quest.state);
    }

    public void UpdateSelectedList() {
        selectedList.UpdateList();
    }

    public void AddAntSelection(AntManager.AntType type) {
        holder.AddAntSelection(type);
    }

    public void DecreaseAntSelection(AntManager.AntType type) {
        holder.DecreaseAntSelection(type);
    }

    public void ResetSelection() {
        holder.ResetSelection();
    }

    public void OnDisable() {
        Destroy(gameObject);
    }

    public void OnDestroy() {
        holder.onSelectedAntsChange -= UpdateSelectedList;
        holder.quest.onStateChanged -= UpdateButton;
    }
}
