using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SphereRecallButton : MonoBehaviour
{

    public XRBaseInteractor socket;
    public XRBaseInteractable sphereInteractable;
    public SphereManager sphereManager;

    // Start is called before the first frame update
    void Start()
    {
        sphereInteractable = GameObject.FindWithTag("TeleportSphere").GetComponent<XRBaseInteractable>();
        sphereManager = sphereInteractable.GetComponent<SphereManager>();
    }

    public void RecallSphere() {
        Debug.Log("Triggering recall!");
        XRBaseInteractor interactor = sphereInteractable.selectingInteractor;
        if (interactor) {
            interactor.enableInteractions = false;
        //     SelectExitEventArgs args = new SelectExitEventArgs();
        //     args.interactable = sphereInteractable;
        //     args.interactor = sphereInteractable.selectingInteractor;
        //     sphereInteractable.selectExited?.Invoke(args);
        }
        sphereInteractable.transform.position = socket.attachTransform.position;
        StartCoroutine(ReenableInteractor(interactor));
    }

    IEnumerator ReenableInteractor(XRBaseInteractor interactor)  {
        yield return null;
        interactor.enableInteractions = true;
    }

}
