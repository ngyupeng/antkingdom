using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResources 
{

    public enum ResourceType {
        Wood,
        Stone,
        Food,
        Iron,
        MagicCrystal
    }
    
    public delegate void OnResourceAmountChanged();
    public static event OnResourceAmountChanged onResourceAmountChanged;

    public delegate void OnNotEnoughResources();
    public static event OnNotEnoughResources onNotEnoughResources;
    public delegate void OnResourceCapacityChanged();
    public static event OnResourceCapacityChanged onResourceCapacityChanged;
    private static Dictionary<ResourceType, int> resourceAmountData;
    private static Dictionary<ResourceType, int> resourceCapacity;
    private static Dictionary<ResourceType, Resource> resourceMap;

    public static void Init() {
        resourceAmountData = new Dictionary<ResourceType, int>();
        resourceCapacity = new Dictionary<ResourceType, int>();
        resourceMap = new Dictionary<ResourceType, Resource>();
        foreach (ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType))) {
            resourceAmountData[resourceType] = 500;
            resourceCapacity[resourceType] = 500;
        }
        
        string resourcePath = @"Game Resources\";
        Resource[] all = Resources.LoadAll<Resource>(resourcePath);
        foreach (var resource in all) {
            resourceMap[resource.GetResourceType()] = resource;
        }
    }

    #region Resource Management
    public static int AddResourceAmount(ResourceType resourceType, int amount) {
        int amountChanged = Mathf.Min(amount, resourceCapacity[resourceType] - resourceAmountData[resourceType]);
        resourceAmountData[resourceType] += amountChanged;
        onResourceAmountChanged?.Invoke();
        return amountChanged;
    }

    public static Resource GetResourceFromType(ResourceType resourceType) {
        return resourceMap[resourceType];
    }

    public static int GetResourceAmount(ResourceType resourceType) {
        return resourceAmountData[resourceType];
    }

    public static int GetResourceCapacity(ResourceType resourceType) {
        return resourceCapacity[resourceType];
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

    // This will only be called when the resources need to be used
    // Used for generating popup text for now
    public static bool RequireResourceAmount(ResourceType resourceType, int amount) {
        bool canAfford = HasResourceAmount(resourceType, amount);

        if (!canAfford) {
            onNotEnoughResources?.Invoke();
            return false;
        }

        return true;
    }

    // This will only be called when the resources need to be used
    // Used for generating popup text for now
    public static bool RequireResourceListAmounts(ResourceCost[] resourceCosts) {
        bool canAfford = HasResourceListAmounts(resourceCosts);

        if (!canAfford) {
            onNotEnoughResources?.Invoke();
            return false;
        }

        return true;
    }

    public static bool UseResourceAmount(ResourceType resourceType, int amount) {
        if (!RequireResourceAmount(resourceType, amount)) return false;

        // Deplete resources
        AddResourceAmount(resourceType, -amount);
        return true;
    }

    public static bool UseResourceListAmounts(ResourceCost[] resourceCosts) {
        if (!RequireResourceListAmounts(resourceCosts)) return false;

        // Deplete resources
        foreach (var resourceCost in resourceCosts) {
            ResourceType resourceType = resourceCost.resource.GetResourceType();
            int amount = resourceCost.cost;
            AddResourceAmount(resourceType, -amount);
        }
        return true;
    }

    public static void GetResourceListAmounts(ResourceCost[] resourceCosts) {
        foreach (var resourceCost in resourceCosts) {
            ResourceType resourceType = resourceCost.resource.GetResourceType();
            int amount = resourceCost.cost;
            AddResourceAmount(resourceType, amount);
        }
    }

    #endregion

    #region Storage Management

    public static void IncreaseStorage(ResourceType resourceType, int amount) {
        resourceCapacity[resourceType] += amount;
        onResourceCapacityChanged?.Invoke();
    }

    #endregion
}
