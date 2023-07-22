using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestReward
{
    public GameResources.ResourceType resourceType;
    public float baseChance;
    public float multiplier;
    public int minAmount;
    public int maxAmount;

    public float ComputeChance(float efficiency) {
        return Mathf.Min(baseChance + multiplier * efficiency, 100f);
    }
}
