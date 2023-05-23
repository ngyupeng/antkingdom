using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    Vector3 dragOrigin;
    private void Update() {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
        }
    }

    private void ZoomCamera() {
        float newSize = cam.orthographicSize - zoomStep * Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }
}
