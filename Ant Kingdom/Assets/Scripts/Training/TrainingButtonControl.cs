using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButtonControl : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject inProgressText;
    public GameObject unlockedText;
    public TrainingPanel panel;
    public void UpdateButton(AntData ant) {
        upgradeButton.SetActive(false);
        inProgressText.SetActive(false);
        unlockedText.SetActive(false);
        if (AntManager.GetAntUnlocked(ant.antType)) {
            unlockedText.SetActive(true);
        } else if (panel.isActive) {
            inProgressText.SetActive(true);
        } else {
            upgradeButton.SetActive(true);
        }
    }
}
