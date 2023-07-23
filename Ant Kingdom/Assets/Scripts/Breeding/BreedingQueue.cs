using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingQueue : MonoBehaviour
{ 
    public GameObject breedingInstancePrefab;
    public Transform content;

    void Awake()
    {
        SaveSystem.breeding.Add(this);
    }
    
    public void AddBreeding(AntData antData) {
        BreedingInstance lastQueuedBreeding = null;
        if (content.childCount > 0) {
            lastQueuedBreeding = content.GetChild(content.childCount - 1).GetComponent<BreedingInstance>();
        }
        if (lastQueuedBreeding != null && lastQueuedBreeding.antData.antType == antData.antType) {
            lastQueuedBreeding.IncreaseBreeding();
        } else {
            GameObject go = Instantiate(breedingInstancePrefab, content);
            lastQueuedBreeding = go.GetComponent<BreedingInstance>();
            lastQueuedBreeding.Initialise(antData, this);
        }
       
    }

    public void StartBreeding() {
        BreedingInstance firstBreeding = null;
        if (content.childCount == 0) {
            return;
        }
        firstBreeding = content.GetChild(0).GetComponent<BreedingInstance>();
        firstBreeding.StartBreeding();
    }

    public void DestroyInstance(GameObject go) {
        Destroy(go);
        Invoke("StartBreeding", Time.deltaTime);
    }
    public void RefundBreeding() {
        int totalCost = 0;
        foreach (Transform child in content) {
            BreedingInstance bi = child.GetComponent<BreedingInstance>();
            int cost = bi.number * bi.antData.foodCost;
            totalCost += cost;
        }
        GameResources.AddResourceAmount(GameResources.ResourceType.Food, totalCost);
    }
}
