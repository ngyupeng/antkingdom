using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public static AntSpawner instance;
    
    // These are for initialising dictionary
    public AntManager.AntType[] types;
    public GameObject[] antPrefabs;
    public Dictionary<AntManager.AntType, GameObject> antPrefabMap;
    public GameObject nest;

    private void Awake() {
        instance = this;
        antPrefabMap = new Dictionary<AntManager.AntType, GameObject>();
        for (int i = 0; i < types.Length; i++) {
            antPrefabMap[types[i]] = antPrefabs[i];
        }
    }

    public GameObject SpawnAnt(AntManager.AntType type) {
        return Instantiate(antPrefabMap[type], nest.transform.position, Quaternion.identity);
    }
}
