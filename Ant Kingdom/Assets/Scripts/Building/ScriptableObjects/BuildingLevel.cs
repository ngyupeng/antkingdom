using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevel : ScriptableObject
{
    public ResourceCost[] resourceCostsList;
    public float buildTime;
    public Sprite buildingSprite;
    
    public int health;
}
