using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{

    public InputActionReference pointAction;
    public Collider finger;
    public GameObject fingerModel;

    void OnEnable() {
        pointAction.action.started += DoPoint;
        pointAction.action.canceled += StopPoint;
    }

    void OnDisable() {
        pointAction.action.started -= DoPoint;
        pointAction.action.canceled -= StopPoint;
    }

    void DoPoint(InputAction.CallbackContext context) {
        finger.enabled = true;
        fingerModel.SetActive(true);
    }

    void StopPoint(InputAction.CallbackContext context) {
        finger.enabled = false;
        fingerModel.SetActive(false);
    }
}
