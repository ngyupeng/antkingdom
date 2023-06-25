using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{  
    private void Awake()
    {
        GameResources.Init();
        IconDatabase.Init();
        AntManager.Init();
    }
}
