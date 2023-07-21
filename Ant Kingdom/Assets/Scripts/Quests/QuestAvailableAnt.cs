using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestAvailableAnt : MonoBehaviour
{
    private AntData ant;
    private int antCount;
    public TextMeshProUGUI antCountText;
    public Image antImage;
    public QuestInfoPanel panel;

    private void Awake() {
        AntManager.onIdleAntNumberChanged += UpdateView;
    }
    public void Init(AntData antD, QuestInfoPanel nPanel) {
        ant = antD;
        panel = nPanel;
        antImage.sprite = ant.sprite;
        antCount = AntManager.GetIdleAntCount(ant.antType);
        antCountText.text = antCount.ToString() + "x";
    }

    public void UpdateView() {
        antCount = AntManager.GetIdleAntCount(ant.antType);
        antCountText.text = antCount.ToString() + "x";
    }

    public void SelectAnt() {
        panel.AddAntSelection(ant.antType);
    }

    private void OnDestroy() {
        AntManager.onIdleAntNumberChanged -= UpdateView;
    }
}
