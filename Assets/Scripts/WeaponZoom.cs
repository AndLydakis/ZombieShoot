using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] float zoomedInFoV = 20.0f;
    [SerializeField] float zoomedOutFoV = 90.0f;
    [SerializeField] float zoomedOutMouseSens = 2.0f;
    [SerializeField] float zoomedInMouseSens = .5f;
    bool zoomToggled = false;
    RigidbodyFirstPersonController rbfpsc;

    private void Start() {
        rbfpsc = FindObjectOfType<RigidbodyFirstPersonController>();
        ZoomOut();
    }

    void ZoomIn() {
        playerCam.fieldOfView = HorizontalToVerticalFOV(zoomedInFoV);
        rbfpsc.mouseLook.XSensitivity = zoomedInMouseSens;
        rbfpsc.mouseLook.YSensitivity = zoomedInMouseSens;
    }

    void ZoomOut() {
        playerCam.fieldOfView = HorizontalToVerticalFOV(zoomedOutFoV);
        rbfpsc.mouseLook.XSensitivity = zoomedOutMouseSens;
        rbfpsc.mouseLook.YSensitivity = zoomedOutMouseSens;
    }
    private void OnDisable() {
        zoomToggled = false;
        ZoomOut();
    }


    float HorizontalToVerticalFOV(float horizontalFOV) {
        return Mathf.Rad2Deg * 2 * Mathf.Atan(Mathf.Tan((horizontalFOV * Mathf.Deg2Rad) / 2f) / playerCam.aspect);
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            zoomToggled = !zoomToggled;
            if (zoomToggled) {
                ZoomIn();
            }
            else {
                ZoomOut();
            }
        }
    }
}
