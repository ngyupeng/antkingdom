using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestSelectedAnt : MonoBehaviour
{
    private AntData ant;
    private int antCount;
    private int reduceCount = 0;
    public TextMeshProUGUI antCountText;
    public Image antImage;
    public QuestInfoPanel panel;
    public QuestInstance quest;
    private void Awake() {
        AntManager.onIdleAntNumberChanged += UpdateView;
    }
    public void Init(AntData antD, QuestInfoPanel nPanel) {
        ant = antD;
        antImage.sprite = ant.sprite;
        panel = nPanel;
        panel.holder.onSelectedAntsChange += UpdateView;
        quest = panel.holder.quest;
        quest.onStateChanged += UpdateView;
        UpdateView();
    }

    public void UpdateView() {
        antCount = panel.holder.GetSelectedAntCount(ant.antType);
        if (antCount == 0) Destroy(gameObject);
        antCountText.text = antCount.ToString() + "x";
        if (quest.state == QuestInstance.State.Inactive && antCount > AntManager.GetIdleAntCount(ant.antType)) {
            antCountText.color = Color.red;
        } else {
            antCountText.color = Color.black;
        }
    }

    public void DecreaseCount() {
        panel.DecreaseAntSelection(ant.antType);
    }

    private void OnDestroy() {
        AntManager.onIdleAntNumberChanged -= UpdateView;
        panel.holder.onSelectedAntsChange -= UpdateView;
        quest.onStateChanged -= UpdateView;
    }
}
