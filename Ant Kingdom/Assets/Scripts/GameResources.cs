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

    public static bool HasResourceAmount(ResourceType resourceType, int amount) {
        return resourceAmountData[resourceType] >= amount;
    }

    public static bool HasResourceListAmounts(ResourceCost[] resourceCosts) {
        // Check if enough resources
        foreach (var resourceCost in resourceCosts) {
            ResourceType resourceType = resourceCost.resource.GetResourceType();
            int amount = resourceCost.cost;
            if (!HasResourceAmount(resourceType, amount)) {
                return false;
            }
        }

        return true;
    }

    public static bool UseResourceListAmounts(ResourceCost[] resourceCosts) {
        // Check if enough resources
        if (!HasResourceListAmounts(resourceCosts)) return false;

        // Deplete resources
        foreach (var resourceCost in resourceCosts) {
            ResourceType resourceType = resourceCost.resource.GetResourceType();
            int amount = resourceCost.cost;
            AddResourceAmount(resourceType, -amount);
        }
        return true;
    }
}
