using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstance 
{
    public QuestData questData { get; private set; }
    public Dictionary<AntManager.AntType, int> antSelected;
    private Dictionary<AntManager.AntType, int> antSurvived;
    private Dictionary<GameResources.ResourceType, int> resourceCollected;
    public enum State {
        Inactive,
        Active,
        Completed
    }
    public State state;
    public delegate void OnStateChanged();
    public event OnStateChanged onStateChanged;
    public delegate void OnNotEnoughAnts();
    public static event OnNotEnoughAnts onNotEnoughAnts;
    public delegate void OnAntCountOutsideLimits();
    public static event OnAntCountOutsideLimits onAntCountOutsideLimits;
    public QuestInstance(QuestData data) {
        questData = data;
        antSelected = new Dictionary<AntManager.AntType, int>();
        antSurvived = new Dictionary<AntManager.AntType, int>();
        resourceCollected = new Dictionary<GameResources.ResourceType, int>();

        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            antSelected[antType] = 0;
            antSurvived[antType] = 0;
        }

        foreach (QuestReward reward in questData.rewards) {
            GameResources.ResourceType type = reward.resourceType;
            resourceCollected[type] = 0;
        }

        state = State.Inactive;
    }

    public bool StartQuest(QuestData data, Dictionary<AntManager.AntType, int> antSelect, TimerTooltip tooltip) {
        SetDetails(data, antSelect);
        if (!CheckAntCount()) return false;
        SendAnts();
        state = State.Active;
        onStateChanged?.Invoke();
        tooltip.StartTimer();
        return true;
    }

    public void FinishQuest() {
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            AntData ant = AntManager.GetAntData(antType);
            float survivalRate = ant.defence / questData.dangerLevel;
            int antCount = antSelected[antType];
            int surviveCount = 0;
            for (int i = 0; i < antCount; i++) {
                if (RandomSuccess(survivalRate)) {
                    surviveCount++;
                }
            }
            AntManager.AddIdleAnts(antType, surviveCount);
            antSurvived[antType] = surviveCount;
        }

        foreach (QuestReward reward in questData.rewards) {
            GameResources.ResourceType resourceType = reward.resourceType;
            float getChance = reward.chance;
            resourceCollected[resourceType] = 0;
            if (!RandomSuccess(getChance)) continue;
            int resourceGot = Random.Range(reward.minAmount, reward.maxAmount + 1);
            GameResources.AddResourceAmount(resourceType, resourceGot);
            resourceCollected[resourceType] = resourceGot;
        }

        state = State.Completed;
        onStateChanged?.Invoke();
    }

    public void CompleteQuest() {
        state = State.Inactive;
        onStateChanged?.Invoke();
    }

    public void SetDetails(QuestData data, Dictionary<AntManager.AntType, int> antSelect) {
        questData = data;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            antSelected[antType] =  antSelect[antType];
        }
    }

    public bool CheckAntCount() {
        int sum = 0;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            int antCount = antSelected[antType];
            sum += antCount;
            if (!AntManager.HasIdleAntType(antType, antCount)) {
                onNotEnoughAnts?.Invoke();
                return false;
            }
        }

        if (sum > questData.maxAnts || sum < questData.minAnts) {
            onAntCountOutsideLimits?.Invoke();
            return false;
        }

        return true;
    }

    public void SendAnts() {
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            int antCount = antSelected[antType];
            AntManager.UseIdleAnts(antType, antCount);
        }
    }

    public bool RandomSuccess(float chanceOfSuccess) {
        float value = Random.Range(0f, 1f);
        return value <= chanceOfSuccess;
    }

    public int GetSurvivingAnts(AntManager.AntType type) {
        return antSurvived[type];
    }

    public int GetResourceObtained(GameResources.ResourceType type) {
        return resourceCollected[type];
    }
}
