using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public static AntSpawner instance;
    public GameObject antPrefab;
    public GameObject nest;

    private void Awake() {
        instance = this;
    }

    public GameObject SpawnAnt() {
        return Instantiate(antPrefab, nest.transform.position, Quaternion.identity);
    }
}
