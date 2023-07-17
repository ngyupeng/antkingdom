using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleAntCountUI : MonoBehaviour
{
    public TextMeshProUGUI idleAntCount;

    private void Awake() {
        AntManager.onAntNumberChanged += UpdateView;
        AntManager.onIdleAntNumberChanged += UpdateView;
    }

    private void Start() {
        UpdateView();
    }
    public void UpdateView() {
        idleAntCount.text = AntManager.GetTotalIdleAnts().ToString() + " / "  + AntManager.GetTotalAnts().ToString();
    }

    private void OnDestroy() {
        AntManager.onAntNumberChanged -= UpdateView;
        AntManager.onIdleAntNumberChanged -= UpdateView;
    }
}
