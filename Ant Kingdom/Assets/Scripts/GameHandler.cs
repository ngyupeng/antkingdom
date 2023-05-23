using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        GameResources.Init();
        Debug.Log("Handler Awake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
