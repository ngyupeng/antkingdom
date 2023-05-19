using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourcePanelVisibility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool inContext;
    GameObject GO;

    private float activeTime;
 
    private void Awake()
    {
        GO = gameObject;
        activeTime = 0;
    }
 
    void Update()
    {
        activeTime += Time.deltaTime;
        if (activeTime > Time.deltaTime * 5 && Input.GetMouseButtonDown(0) && !inContext) {
            GO.SetActive(inContext);
            activeTime = 0;
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        inContext = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        inContext = false;
    }
}
