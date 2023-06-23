using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveButton : MonoBehaviour
{
    public delegate void OnClickedMove();
    public static event OnClickedMove onClickedMove;
    public void MoveBuilding() {
        onClickedMove?.Invoke();
    }
}
