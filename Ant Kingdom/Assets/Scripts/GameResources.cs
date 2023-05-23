using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResources 
{

    public enum ResourceType {
        Wood,
        Stone,
    }
    
    public delegate void OnResourceAmountChanged();
    public static event OnResourceAmountChanged onResourceAmountChanged;
    private static Dictionary<ResourceType, int> resourceAmountData;

    public static void Init() {
        resourceAmountData = new Dictionary<ResourceType, int>();
        foreach (ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType))) {
            resourceAmountData[resourceType] = 0;
        }
    }

    public static void AddResourceAmount(ResourceType resourceType, int amount) {
        resourceAmountData[resourceType] += amount;
        onResourceAmountChanged?.Invoke();
    }

    public static int GetResourceAmount(ResourceType resourceType) {
        return resourceAmountData[resourceType];
    }
}
