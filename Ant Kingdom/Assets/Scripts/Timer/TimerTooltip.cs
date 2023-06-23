using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerTooltip : MonoBehaviour
{
    public GameObject attachedObject;
    private RectTransform rectTransform;
    private Camera uiCamera;
    public Timer timer;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI timeLeftText;

    public void Initialise(GameObject go, RectTransform rectTrans, Camera cam) {
        attachedObject = go;
        rectTransform = rectTrans;
        uiCamera = cam;
        timer = gameObject.AddComponent<Timer>();
    }
    public void InitTimer(TimeSpan time) {
        timer.Initialise(time);
    }
    private void Update() {
        Vector2 initPosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(attachedObject.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint,
            uiCamera, out initPosition);
        gameObject.transform.localPosition = initPosition;
        FixedUpdate();
    }

    private void FixedUpdate() {
        progressSlider.value = (float) (1.0 - timer.secondsLeft / timer.timeToFinish.TotalSeconds);
        timeLeftText.text = timer.DisplayTime();
    }
}
