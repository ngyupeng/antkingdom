using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ResourceData
{
    public Dictionary<GameResources.ResourceType, int> resourceAmountData;
    public Dictionary<GameResources.ResourceType, int> resourceCapacity;

    public ResourceData()
    {
        resourceAmountData = GameResources.resourceAmountData;
        resourceCapacity = GameResources.resourceCapacity;
    }

}
