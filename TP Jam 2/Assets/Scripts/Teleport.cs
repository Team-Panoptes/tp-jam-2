using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{

    public InputActionReference teleportAction;
    public Transform teleportSphere;
    public Transform playerTransform;
    public Camera mainCamera;
    private SphereManager sphereManager;


    // Start is called before the first frame update
    void Start()
    {
        teleportSphere = GameObject.FindWithTag("TeleportSphere").transform;
        mainCamera = Camera.main;
        sphereManager = teleportSphere.GetComponent<SphereManager>();
    }

    void OnEnable() {
        teleportAction.action.performed += DoTeleport;
    }

    void OnDisable() {
        teleportAction.action.performed -= DoTeleport;
    }

    void DoTeleport(InputAction.CallbackContext context) {
        if (sphereManager.isActive) {
            Debug.Log("TELEPORT!");
            playerTransform.position = teleportSphere.position - Vector3.ProjectOnPlane(mainCamera.transform.localPosition, Vector3.up) - Vector3.up * 0.05f;
        }
    }
}
