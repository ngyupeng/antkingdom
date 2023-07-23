using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTrainButton : MonoBehaviour
{
    public delegate void OnClickedTrain();
    public static event OnClickedTrain onClickedTrain;

    public void OpenTrainingScreen() {
        onClickedTrain?.Invoke();
    }
}
