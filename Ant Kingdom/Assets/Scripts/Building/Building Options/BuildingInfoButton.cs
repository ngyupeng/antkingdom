using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoButton : MonoBehaviour
{
    public delegate void OnClickedInfo();
    public static event OnClickedInfo onClickedInfo;
    public void ShowBuildingInfo() {
        onClickedInfo?.Invoke();
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
