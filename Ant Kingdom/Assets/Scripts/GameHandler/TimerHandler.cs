using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    public static TimerHandler instance;
    private void Awake() {
        instance = this;
    }

    public Timer CreateTimer() {
        return gameObject.AddComponent<Timer>();
    }
}
