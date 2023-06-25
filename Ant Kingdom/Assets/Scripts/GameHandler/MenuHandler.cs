using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    void Update()
    {
        // Toggle active
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (menu.activeSelf) {
                GridBuildingSystem.current.setMenuInactive();
            } else {
                GridBuildingSystem.current.setMenuActive();
            }
            menu.SetActive(!menu.activeSelf);
            menu.transform.SetAsLastSibling();
        }
    }
}
