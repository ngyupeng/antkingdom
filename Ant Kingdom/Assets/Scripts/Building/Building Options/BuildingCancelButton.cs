using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCancelButton : MonoBehaviour
{
    public delegate void OnClickedCancel();
    public static event OnClickedCancel onClickedCancel;

    public void CancelBuilding() {
        onClickedCancel?.Invoke();
    }
}
