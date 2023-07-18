using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestReward
{
    public GameResources.ResourceType resourceType;
    public float chance;
    public int minAmount;
    public int maxAmount;
}
