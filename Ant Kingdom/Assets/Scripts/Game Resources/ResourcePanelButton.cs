using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePanelButton : MonoBehaviour
{
    public delegate void OnButtonClicked();
    public static event OnButtonClicked onButtonClicked;

    public void doStuff() {
        onButtonClicked?.Invoke();
    }
}
