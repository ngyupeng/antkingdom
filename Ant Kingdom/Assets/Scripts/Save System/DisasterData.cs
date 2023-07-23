using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisasterData
{
    public float minDamage;
    public float maxDamage;
    public float timeToNextDisaster;
    public bool isGameOver;

    public DisasterData()
    {
        minDamage = DisasterSystem.minDamage;
        maxDamage = DisasterSystem.maxDamage;
        timeToNextDisaster = DisasterSystem.timeToNextDisaster;
        isGameOver = DisasterSystem.isGameOver;
    }
}
