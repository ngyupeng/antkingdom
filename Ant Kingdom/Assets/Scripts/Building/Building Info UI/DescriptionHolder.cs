using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionHolder : MonoBehaviour
{
    public GameObject description;
    public GameObject removeConfirmation;

    private void Awake() {
        AntCountInstance.onDecreaseCount += ToggleOnRemove;
    }
    public void ToggleOnDescription() {
        description.SetActive(true);
        removeConfirmation.SetActive(false);
    }

    public void ToggleOnRemove() {
        description.SetActive(false);
        removeConfirmation.SetActive(true);
    }

    private void OnDestroy() {
        AntCountInstance.onDecreaseCount -= ToggleOnRemove;
    }
}
