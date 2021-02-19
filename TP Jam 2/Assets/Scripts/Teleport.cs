using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{

    public InputActionReference teleportAction;
    public Transform teleportSphere;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        teleportSphere = GameObject.FindWithTag("TeleportSphere").transform;
    }

    void OnEnable() {
        teleportAction.action.performed += DoTeleport;
    }

    void OnDisable() {
        teleportAction.action.performed -= DoTeleport;
    }

    void DoTeleport(InputAction.CallbackContext context) {
        Debug.Log("TELEPORT!");
        playerTransform.position = teleportSphere.position;
    }
}
