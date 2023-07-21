using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAntCountList : MonoBehaviour
{
    public Transform content;
    public GameObject antCountInstancePrefab;
    private Dictionary<AntManager.AntType, bool> hasType; 
    public QuestInfoPanel panel;

    private void Awake() {
        hasType = new Dictionary<AntManager.AntType, bool>();
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            hasType[antType] = false;
        }
        AntManager.onIdleAntNumberChanged += UpdateList;
    }

    private void Start() {
        UpdateList();
    }

    public void UpdateList() {
        int index = 0;
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            if (AntManager.GetIdleAntCount(antType) > 0) {
                index++;
                if (!hasType[antType]) {
                    CreateInstance(antType, index - 1);
                    hasType[antType] = true;
                }
            } else {
                hasType[antType] = false;
            }
        }
    }

    public void CreateInstance(AntManager.AntType type, int index = -1) {
        GameObject go = Instantiate(antCountInstancePrefab, content);
        QuestAvailableAnt instance = go.GetComponent<QuestAvailableAnt>();
        instance.Init(AntManager.GetAntData(type), panel);
        if (index != -1) {
            go.transform.SetSiblingIndex(index);
        }
    }

    private void OnDestroy() {
        AntManager.onIdleAntNumberChanged -= UpdateList;
    }
}
