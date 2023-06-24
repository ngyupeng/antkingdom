using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameObjects/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public ResourceCost[] resourceCostsList { get; private set; }

    public float buildTime { get; private set; }
    public BuildingType type;
    public Sprite icon {get; private set; }

    public GameObject prefab;
    public BuildingStates states;

    public void Init() {
        resourceCostsList = states.levels[0].resourceCostsList;
        buildTime = states.levels[0].buildTime;
        icon = states.levels[0].buildingSprite;
    }
}

public enum BuildingType {
    ResourceBuilding,
    Housing
}