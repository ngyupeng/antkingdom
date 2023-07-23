using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingPanel : MonoBehaviour
{
    public TrainingAntDetailsPanel detailsPanel;
    public bool isActive = false;
    public Transform trainingChoiceList;
    public Transform trainingInstance;
    public GameObject trainingChoicePrefab;
    public GameObject trainingInstancePrefab;
    public delegate void OnStateChanged();
    public static event OnStateChanged onStateChanged;

    public void Start() {
        foreach (AntManager.AntType type in System.Enum.GetValues(typeof(AntManager.AntType))) {
            GameObject go = Instantiate(trainingChoicePrefab, trainingChoiceList);
            TrainingChoice choice = go.GetComponent<TrainingChoice>();
            choice.Init(AntManager.GetAntData(type), this);
        }
    }
    public void DisplayAntInfo(AntData ant) {
        detailsPanel.gameObject.SetActive(true);
        detailsPanel.Initialise(ant, this);
    }

    public void StartTraining(AntData ant) {
        isActive = true;
        GameObject go = Instantiate(trainingInstancePrefab, trainingInstance);
        TrainingInstance instance = go.GetComponent<TrainingInstance>();
        instance.Init(ant, this);
        onStateChanged?.Invoke();
    }

    public void FinishTraining(AntData ant) {
        isActive = false;
        AntManager.UnlockAnt(ant.antType);
        onStateChanged?.Invoke();
    }

    public void CancelTraining() {
        isActive = false;
        onStateChanged?.Invoke();
    }
} 
