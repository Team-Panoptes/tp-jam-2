using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{

    public InputActionReference pointAction;
    public InputActionReference hornsAction;

    public Collider finger;
    public GameObject fingerModel;
    public Animator animator;

    string lastTrigger = "idle";


    void OnEnable() {
        pointAction.action.started += DoPoint;
        pointAction.action.canceled += StopPoint;
        hornsAction.action.started += DoHorns;
        hornsAction.action.canceled += StopGesture;
    }

    void OnDisable() {
        pointAction.action.started -= DoPoint;
        pointAction.action.canceled -= StopPoint;
        hornsAction.action.started -= DoHorns;
        hornsAction.action.canceled -= StopGesture;
    }

    void DoPoint(InputAction.CallbackContext context) {
        finger.enabled = true;
        //fingerModel.SetActive(true);
        animator.SetTrigger("point");
        lastTrigger = "point";  
    }

    void StopPoint(InputAction.CallbackContext context) {
        finger.enabled = false;
        //fingerModel.SetActive(false);
        animator.SetTrigger("idle");
        lastTrigger = "idle";
    }

    void DoHorns(InputAction.CallbackContext context) {
        if (lastTrigger == "idle") {
            animator.SetTrigger("horns");
            lastTrigger = "horns";
        }
    }

    void StopGesture(InputAction.CallbackContext context) {
        if (lastTrigger != "point") {
            animator.SetTrigger("idle");
            lastTrigger = "idle";
        }
    }

}
