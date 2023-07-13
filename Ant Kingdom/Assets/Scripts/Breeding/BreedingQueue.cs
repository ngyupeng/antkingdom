using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingQueue : MonoBehaviour
{ 
    public GameObject breedingInstancePrefab;
    public Transform content;
    
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
}
