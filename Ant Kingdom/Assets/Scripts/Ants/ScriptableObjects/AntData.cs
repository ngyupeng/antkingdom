using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ant Data", menuName = "Assets/Ant Data")]
public class AntData : ScriptableObject
{
    public string antName;
    public int foodCost;
    public float breedingTime;
    public Sprite sprite;
    public AntManager.AntType antType;
}
