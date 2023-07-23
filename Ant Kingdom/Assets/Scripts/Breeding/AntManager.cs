using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AntManager 
{
    public enum AntType {
        WorkerAnt,
        EngineerAnt,
        SoldierAnt
    }
    public static Dictionary<AntType, int> antCount;
    public static Dictionary<AntType, int> idleAntCount;
    public static Dictionary<AntType, AntData> antDataMap;
    public static Dictionary<AntType, bool> antUnlocked;
    public static int totalAnts;
    public static int idleAnts;
    public static int antCapacity;
    public delegate void OnAntNumberChanged();
    public static event OnAntNumberChanged onAntNumberChanged;
    public delegate void OnIdleAntNumberChanged();
    public static event OnIdleAntNumberChanged onIdleAntNumberChanged;
    public delegate void OnNoIdleAnts();
    public static event OnNoIdleAnts onNoIdleAnts;
    public delegate void OnAntUnlocked();
    public static event OnAntUnlocked onAntUnlocked;
    public static void Init() {
        antCount = new Dictionary<AntType, int>();
        idleAntCount = new Dictionary<AntType, int>();
        antDataMap = new Dictionary<AntType, AntData>();
        antUnlocked = new Dictionary<AntType, bool>();

        totalAnts = 5;
        idleAnts = 5;
        antCapacity = 10;
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            if (antType == AntType.WorkerAnt) {
                antCount[antType] = 5;
                idleAntCount[antType] = 5;
                antUnlocked[antType] = true;
            } else {
                antCount[antType] = 0;
                idleAntCount[antType] = 0;
                antUnlocked[antType] = false;
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

    public static int GetAntCount(AntType antType) {
        return antCount[antType];
    }

    public static int GetIdleAntCount(AntType antType) {
        return idleAntCount[antType];
    }

    public static int GetTotalIdleAnts() {
        return idleAnts;
    }
    public static AntData GetAntData(AntType antType) {
        return antDataMap[antType];
    }

    public static bool GetAntUnlocked(AntType antType) {
        return antUnlocked[antType];
    }

    public static void UnlockAnt(AntType antType) {
        antUnlocked[antType] = true;
        onAntUnlocked?.Invoke();
    }

    public static void AddAntAmount(AntType antType, int amount) {
        antCount[antType] += amount;
        totalAnts += amount;
        AddIdleAnts(antType, amount);
        onAntNumberChanged?.Invoke();
    }

    public static int GetTotalCapacity() {
        return antCapacity;
    }

    public static void AddAntCapacity(int amount) {
        antCapacity += amount;
        FixExcessCapacity();
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

    public static bool HasIdleAntType(AntType type, int amount) {
        if (idleAntCount[type] < amount) {
            return false;
        }
        return true;
    }

    public static AntType FindIdleAnt() {
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            if (idleAntCount[antType] > 0) {
                return antType;
            }
        }
        // shouldn't reach here, only call function after HasIdleAnt
        return AntType.WorkerAnt;
    }

    public static bool UseRandomIdleAnt() {
        if (!HasIdleAnt()) return false;
        AntType type = FindIdleAnt();
        UseIdleAnts(type, 1);
        return true;
    }

    public static bool UseIdleAnts(AntType type, int amount) {
        if (!HasIdleAntType(type, amount)) {
            return false;
        }
        idleAnts -= amount;
        idleAntCount[type] -= amount;
        onIdleAntNumberChanged?.Invoke();
        return true;
    }

    public static void AddIdleAnt(AntType type) {
        AddIdleAnts(type, 1);
    }

    public static void AddIdleAnts(AntType type, int amount) {
        idleAntCount[type] += amount;
        idleAnts += amount;
        FixExcessCapacity();
        onIdleAntNumberChanged?.Invoke();
    }

    public static float GetTotalDefence() {
        float sum = 0;
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            sum += idleAntCount[antType] * antDataMap[antType].defence;
        }
        return sum;
    }

    public static void FixExcessCapacity() {
        if (antCapacity >= idleAnts) {
            return;
        }
        while (antCapacity < idleAnts) {
            RemoveRandomIdleAnt();
        }
    }

    public static void RemoveRandomIdleAnt() {
        int value = Random.Range(1, idleAnts + 1);
        int currentSum = 0;
        foreach (AntType antType in System.Enum.GetValues(typeof(AntType))) {
            currentSum += GetIdleAntCount(antType);
            if (currentSum >= value) {
                AddIdleAnts(antType, -1);
                return;
            }
        }
    }
}

