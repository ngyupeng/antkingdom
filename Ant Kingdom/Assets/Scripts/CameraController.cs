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

    [SerializeField]
    private Collider2D boundary;

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
            Vector3 targetPosition = cam.transform.position + difference;
            targetPosition.x = Mathf.Clamp(targetPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, boundary.bounds.min.y, boundary.bounds.max.y);
            cam.transform.position = targetPosition;
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
