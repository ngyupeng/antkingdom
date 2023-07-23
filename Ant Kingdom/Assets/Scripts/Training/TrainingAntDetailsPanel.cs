using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingAntDetailsPanel : MonoBehaviour
{
    public AntData ant;
    public TextMeshProUGUI antName;
    public TextMeshProUGUI trainingTime;
    public Image antImage;
    public Transform statList;
    public Transform resourceList;
    public GameObject trainingStatHolderPrefab;
    public GameObject upgradeCostHolderPrefab;
    public GameObject noneText;
    public TrainingPanel panel;
    public TrainingButtonControl buttonControl;

    private void Awake() {
        TrainingPanel.onStateChanged += UpdateButton;
    }
    public void Initialise(AntData antData, TrainingPanel tpanel) {
        ant = antData;
        panel = tpanel;
        foreach (Transform child in statList) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in resourceList) {
            Destroy(child.gameObject);
        }

        antName.text = ant.antName;
        if (ant.trainingTime >= 60) {
            int totalTime = (int) ant.trainingTime;
            int minutes = totalTime / 60;
            int seconds = totalTime % 60;
            trainingTime.text = minutes.ToString() + "m ";
            if (seconds > 0) {
                trainingTime.text += seconds.ToString() + "s";
            }
        } else if (ant.trainingTime > 0) {
            trainingTime.text = ant.trainingTime.ToString() + "s";
        } else {
            trainingTime.text = "N/A";
        }
        antImage.sprite = ant.sprite;

        DisplayStats();
        DisplayCosts();
        UpdateButton();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void DisplayStats() {
        CreateStat("Defence", ant.defence.ToString());
        CreateStat("Explore Efficiency", ant.exploreEfficiency.ToString());
        CreateStat("Food Cost", ant.foodCost.ToString());
        CreateStat("Breeding Time", ant.breedingTime.ToString() + "s");
    }

    public void CreateStat(string name, string amount) {
        GameObject go = Instantiate(trainingStatHolderPrefab, statList);
        TrainingStatHolder holder = go.GetComponent<TrainingStatHolder>();
        holder.Initialise(name, amount);
    }

    public void DisplayCosts() {
        if (ant.trainingResourceCosts == null || ant.trainingResourceCosts.Length == 0) {
            Instantiate(noneText, resourceList);
            return;
        }

        foreach (ResourceCost cost in ant.trainingResourceCosts) {
            CreateCost(cost);
        }
    }

    public void CreateCost(ResourceCost cost) {
        GameObject go = Instantiate(upgradeCostHolderPrefab, resourceList);
        ResourceCostHolder holder = go.GetComponent<ResourceCostHolder>();
        holder.Initialise(cost.resource, cost.cost);
    }

    public void UpdateButton() {
        buttonControl.UpdateButton(ant);
    }

    public void StartTraining() {
        if (!GameResources.UseResourceListAmounts(ant.trainingResourceCosts)) return;
        panel.StartTraining(ant);
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        TrainingPanel.onStateChanged -= UpdateButton;
    }
}
