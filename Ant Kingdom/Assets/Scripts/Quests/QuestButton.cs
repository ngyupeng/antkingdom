using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    public GameObject startButton;
    public GameObject inProgressButton;
    public GameObject viewRewardsButton;

    public void UpdateButton(QuestInstance.State state) {
        startButton.SetActive(false);
        inProgressButton.SetActive(false);
        viewRewardsButton.SetActive(false);
        switch (state) {
            case QuestInstance.State.Inactive:
                startButton.SetActive(true);
                break;
            case QuestInstance.State.Active:
                inProgressButton.SetActive(true);
                break;
            case QuestInstance.State.Completed:
                viewRewardsButton.SetActive(true);
                break;
        }
    }
}
