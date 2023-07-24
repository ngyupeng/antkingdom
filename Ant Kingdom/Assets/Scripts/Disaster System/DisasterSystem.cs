using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSystem : MonoBehaviour
{
    public static List<Building> buildings = new List<Building>();
    public static List<Building> buildingsToRemove = new List<Building>();
    public static float minDamage;
    public static float maxDamage;
    public static float timeToNextDisaster;
    public DisasterInfo disasterInfo;
    public GameObject popup;
    public static bool isGameOver = true;

    private void Awake() {
        minDamage = 20000;
        maxDamage = 30000;
        timeToNextDisaster = 300f;
        buildings = new List<Building>();
        buildingsToRemove = new List<Building>();
    }
    private void Update() {
        if (isGameOver) return;
        timeToNextDisaster -= Time.deltaTime;
        if (timeToNextDisaster <= 0) {
            timeToNextDisaster = Random.Range(300f, 350f);
            ActivateDisaster();
            minDamage += Random.Range(10000, 15000);
            maxDamage += Random.Range(17000, 22000);
            disasterInfo.UpdateText();
        }
    }

    public void ActivateDisaster() {
        buildingsToRemove.Clear();
        float totalDefence = AntManager.GetTotalDefence() + 100;
        foreach (Building building in buildings) {
            float damage = Random.Range(minDamage, maxDamage) / totalDefence;
            building.ReceiveDamage(damage);
        }
        foreach (Building building in buildingsToRemove) {
            buildings.Remove(building);
        }
        popup.SetActive(true);
    }
}
