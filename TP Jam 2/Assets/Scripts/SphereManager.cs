using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SphereManager : MonoBehaviour
{

    private Renderer renderer;
    public Light pointLight;

    public Color readyColor;
    public Color inactiveColor;

    private XRBaseInteractable interactable;
    public Rigidbody rigidBody;

    public XRBaseInteractor interactor;

    private bool active;
    public bool isActive
    {
        get { return active; }
        set { 
            if (active != value) {
                if (value) {
                    pointLight.color = readyColor;
                } else {
                    pointLight.color = inactiveColor;
                }
            }
            active = value;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelected);
        interactable.selectExited.AddListener(OnDeselected);
    }

    void OnSelected(SelectEnterEventArgs context) {
        interactor = context.interactor;
    }

    void OnDeselected(SelectExitEventArgs context) {
        interactor = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (interactable.isSelected || rigidBody.velocity.magnitude > 0.1f) {
            isActive = false;
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.06f)) {
            isActive = true;
        } else {
            isActive = false;
        }
    }
}
