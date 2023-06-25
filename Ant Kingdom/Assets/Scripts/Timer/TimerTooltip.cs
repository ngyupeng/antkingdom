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

    public void InitialiseWithBuilding(GameObject go, RectTransform rectTrans, Camera cam) {
        attachedObject = go;
        rectTransform = rectTrans;
        uiCamera = cam;
        timer = TimerHandler.instance.CreateTimer();
    }
    public void Init() {
        timer = TimerHandler.instance.CreateTimer();
    }
    public void InitTimer(TimeSpan time) {
        timer.Initialise(time);
    }

    public void RestartTimer() {
        timer.StartTimer();
    }

    private void Update() {
        if (attachedObject != null) {
            Vector2 initPosition;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(attachedObject.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint,
                uiCamera, out initPosition);
            gameObject.transform.localPosition = initPosition;
        }
        FixedUpdate();
    }

    private void FixedUpdate() {
        progressSlider.value = (float) (1.0 - timer.secondsLeft / timer.timeToFinish.TotalSeconds);
        timeLeftText.text = timer.DisplayTime();
    }

    private void OnDestroy() {
        Destroy(timer);
    }
}
