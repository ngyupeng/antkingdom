using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntCountList : MonoBehaviour
{
    public Transform content;
    public GameObject antCountInstancePrefab;
    private Dictionary<AntManager.AntType, bool> hasType; 

    private void Awake() {
        hasType = new Dictionary<AntManager.AntType, bool>();
        AntManager.onIdleAntNumberChanged += UpdateList;
    }

    public void OnEnable() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
        foreach (AntManager.AntType antType in System.Enum.GetValues(typeof(AntManager.AntType))) {
            if (AntManager.GetIdleAntCount(antType) > 0) {
                hasType[antType] = true;
                CreateInstance(antType);
            } else {
                hasType[antType] = false;
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
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
            }
        }
    }

    public void ConfirmReduce() {
        foreach (Transform child in content) {
            AntCountInstance countInstance = child.gameObject.GetComponent<AntCountInstance>();
            countInstance.ConfirmReduce();
        }
    }

    public void CancelReduce() {
        foreach (Transform child in content) {
            AntCountInstance countInstance = child.gameObject.GetComponent<AntCountInstance>();
            countInstance.Reset();
        }
    }
    public void CreateInstance(AntManager.AntType type, int index = -1) {
        GameObject go = Instantiate(antCountInstancePrefab, content);
        AntCountInstance instance = go.GetComponent<AntCountInstance>();
        instance.Init(AntManager.GetAntData(type));
        if (index != -1) {
            go.transform.SetSiblingIndex(index);
        }
    }

    private void OnDestroy() {
        AntManager.onIdleAntNumberChanged -= UpdateList;
    }
}
