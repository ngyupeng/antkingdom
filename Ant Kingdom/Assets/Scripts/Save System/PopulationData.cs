using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopulationData
{
    public  Dictionary<AntManager.AntType, int> antCount;
    public  Dictionary<AntManager.AntType, int> idleAntCount;
    public  Dictionary<AntManager.AntType, bool> antUnlocked;
    public  int totalAnts;
    public  int idleAnts;
    public  int antCapacity;

    public PopulationData()
    {
        antCount = AntManager.antCount;
        idleAntCount = AntManager.antCount;
        antUnlocked = AntManager.antUnlocked;
        totalAnts = AntManager.totalAnts;
        idleAnts = AntManager.idleAnts;
        antCapacity = AntManager.antCapacity;
    }
}
