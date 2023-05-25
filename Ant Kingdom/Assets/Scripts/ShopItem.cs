using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameObjects/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public ResourceCost[] resourceCostsList;

    public float buildTime;
    public BuildingType type;
    public Sprite icon;

    public GameObject prefab;
}

public enum BuildingType {
    ResourceBuilding,
    Housing
}