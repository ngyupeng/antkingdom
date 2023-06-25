using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AntManager 
{
    public enum AntType {
        WorkerAnt
    }
    private static Dictionary<AntType, int> antCount;
    private static int totalAnts;
    private static int antCapacity;
    public delegate void OnAntNumberChanged();
    public static event OnAntNumberChanged onAntNumberChanged;
    public static void Init() {
        antCount = new Dictionary<AntType, int>();
        totalAnts = 5;
        antCapacity = 10;
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            if (antType == AntType.WorkerAnt) {
                antCount[antType] = 5;
            } else {
                antCount[antType] = 0;
            }
        }
    }

    public static int GetTotalAnts() {
        return totalAnts;
    }
    public static void AddAntAmount(AntType antType, int amount) {
        antCount[antType] += amount;
        totalAnts += amount;
        onAntNumberChanged?.Invoke();
    }

    public static int GetTotalCapacity() {
        return antCapacity;
    }
    public static void AddAntCapacity(int amount) {
        antCapacity += amount;
        onAntNumberChanged?.Invoke();       
    }
    public static bool CanAddAnts(int amount) {
        return antCapacity - totalAnts >= amount;
    }
}

