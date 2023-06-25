using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreedingPanel : MonoBehaviour
{
    private BreedingInstance lastQueuedBreeding;
    public GameObject breedingQueue;
    public GameObject breedingInstancePrefab;
    public TextMeshProUGUI antNumberText;
    
    private void Awake() {
        AntManager.onAntNumberChanged += UpdateAntNumberText;
    }

    public void Start() {
        UpdateAntNumberText();
    }
    public void AddAnt(AntData antData) {
        if (!GameResources.UseResourceAmount(GameResources.ResourceType.Food, antData.foodCost)) {
            return;
        }
        if (lastQueuedBreeding != null && lastQueuedBreeding.antData.antType == antData.antType) {
            lastQueuedBreeding.IncreaseBreeding();
        } else {
            GameObject go = Instantiate(breedingInstancePrefab, breedingQueue.transform);
            lastQueuedBreeding = go.GetComponent<BreedingInstance>();
            lastQueuedBreeding.Initialise(antData);
        }
    }

    private void UpdateAntNumberText() {
        antNumberText.text = "Ants: " + AntManager.GetTotalAnts().ToString() + " / " + AntManager.GetTotalCapacity().ToString();
    }
    private void OnDestroy() {
        AntManager.onAntNumberChanged -= UpdateAntNumberText;
    }
}
