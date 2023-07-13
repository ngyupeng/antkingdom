using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreedingPanel : MonoBehaviour
{
    public BreedingQueue breedingQueue;
    public GameObject breedingChoicePrefab;
    public GameObject breedingChoices;
    public TextMeshProUGUI antNumberText;
    
    private void Awake() {
        AntManager.onAntNumberChanged += UpdateAntNumberText;
        AntManager.onAntUnlocked += UpdateBreedingChoices;
    }

    public void Start() {
        UpdateAntNumberText();
        UpdateBreedingChoices();
    }
    public void AddAnt(AntData antData) {
        if (!GameResources.UseResourceAmount(GameResources.ResourceType.Food, antData.foodCost)) {
            return;
        }
        breedingQueue.AddBreeding(antData);
        
    }

    private void UpdateAntNumberText() {
        antNumberText.text = "Ants: " + AntManager.GetTotalAnts().ToString() + " / " + AntManager.GetTotalCapacity().ToString();
    }

    private void UpdateBreedingChoices() {
        foreach (Transform child in breedingChoices.transform) {
            Destroy(child.gameObject);
        }
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            if (AntManager.GetAntUnlocked(antType)) {
                GameObject go = Instantiate(breedingChoicePrefab, breedingChoices.transform);
                BreedingChoice choice = go.GetComponent<BreedingChoice>();
                choice.Init(AntManager.GetAntData(antType), this);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(breedingChoices.GetComponent<RectTransform>());
    }
    private void OnDestroy() {
        AntManager.onAntNumberChanged -= UpdateAntNumberText;
        AntManager.onAntUnlocked -= UpdateBreedingChoices;
    }
}
