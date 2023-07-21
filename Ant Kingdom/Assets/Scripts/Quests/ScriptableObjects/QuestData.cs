using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Data", menuName = "Assets/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questName;
    public QuestReward[] rewards;
    public int completionTime;
    public int minAnts;
    public int maxAnts;
    public float dangerLevel;
}
