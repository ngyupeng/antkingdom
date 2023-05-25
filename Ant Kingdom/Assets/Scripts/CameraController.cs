using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    Vector3 dragOrigin;

    private bool active = false;
    private void Update() {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            active = true;
        }

        if (Input.GetMouseButton(0) && active) {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }

        else {
            active = false;
        }
    }

    private void ZoomCamera() {
        float newSize = cam.orthographicSize - zoomStep * Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }
}
