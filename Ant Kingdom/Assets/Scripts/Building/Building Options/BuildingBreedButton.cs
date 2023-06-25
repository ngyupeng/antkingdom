using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBreedButton : MonoBehaviour
{
    public delegate void OnClickedBreed();
    public static event OnClickedBreed onClickedBreed;

    public void OpenBreedingScreen() {
        onClickedBreed?.Invoke();
    }
}
