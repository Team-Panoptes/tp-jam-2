using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SphereRecallButton : MonoBehaviour
{

    public XRBaseInteractor socket;
    public XRBaseInteractable sphereInteractable;

    // Start is called before the first frame update
    void Start()
    {
        sphereInteractable = GameObject.FindWithTag("TeleportSphere").GetComponent<XRBaseInteractable>();
    }

    public void RecallSphere() {
        Debug.Log("Triggering recall!");
        sphereInteractable.transform.position = socket.attachTransform.position;
    }

}
