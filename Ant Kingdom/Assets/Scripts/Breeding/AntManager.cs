using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AntManager 
{
    public enum AntType {
        WorkerAnt,
        SoldierAnt,
        ExplorerAnt
    }
    private static Dictionary<AntType, int> antCount;
    private static Dictionary<AntType, AntData> antDataMap;
    private static Dictionary<AntType, bool> antUnlocked;
    private static int totalAnts;
    private static int idleAnts;
    private static int antCapacity;
    public delegate void OnAntNumberChanged();
    public static event OnAntNumberChanged onAntNumberChanged;
    public delegate void OnNoIdleAnts();
    public static event OnNoIdleAnts onNoIdleAnts;
    public delegate void OnAntUnlocked();
    public static event OnAntUnlocked onAntUnlocked;
    public static void Init() {
        antCount = new Dictionary<AntType, int>();
        antDataMap = new Dictionary<AntType, AntData>();
        antUnlocked = new Dictionary<AntType, bool>();

        totalAnts = 5;
        idleAnts = 5;
        antCapacity = 10;
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            if (antType == AntType.WorkerAnt) {
                antCount[antType] = 5;
                antUnlocked[antType] = true;
            } else {
                antCount[antType] = 0;
                antUnlocked[antType] = true;
            }
        }

        string antPath = @"Ants\";
        AntData[] all = Resources.LoadAll<AntData>(antPath);
        foreach (var ant in all) {
            antDataMap[ant.antType] = ant;
        }
    }

    public static int GetTotalAnts() {
        return totalAnts;
    }

    public static AntData GetAntData(AntType antType) {
        return antDataMap[antType];
    }

    public static bool GetAntUnlocked(AntType antType) {
        return antUnlocked[antType];
    }

    public static void UnlockAnt(AntType antType) {
        antUnlocked[antType] = true;
    }

    public static void AddAntAmount(AntType antType, int amount) {
        antCount[antType] += amount;
        totalAnts += amount;
        idleAnts += amount;
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

    public static bool HasIdleAnt() {
        if (idleAnts <= 0) {
            onNoIdleAnts?.Invoke();
            return false;
        }
        return true;
    }

    public static bool UseIdleAnt() {
        if (!HasIdleAnt()) return false;
        idleAnts--;
        return true;
    }

    public static void AddIdleAnt() {
        idleAnts++;
    }
}

