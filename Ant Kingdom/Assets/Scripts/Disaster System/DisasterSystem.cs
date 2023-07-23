using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterSystem : MonoBehaviour
{
    public static List<Building> buildings = new List<Building>();
    public float minDamage = 500;
    public float maxDamage = 1000;
    public float timeToNextDisaster = 600f;
    public DisasterInfo disasterInfo;
    public GameObject popup;
    private void Update() {
        timeToNextDisaster -= Time.deltaTime;
        if (timeToNextDisaster <= 0) {
            timeToNextDisaster = Random.Range(300f, 400f);
            ActivateDisaster();
        }
    }

    public void ActivateDisaster() {
        float totalDefence = AntManager.GetTotalDefence() + 100;
        foreach (Building building in buildings) {
            float damage = Random.Range(minDamage, maxDamage) / totalDefence;
            building.ReceiveDamage(damage);
        }
        popup.SetActive(true);
    }
}
