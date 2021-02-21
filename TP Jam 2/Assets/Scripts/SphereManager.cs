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

    public XRBaseInteractable interactable;
    public Rigidbody rigidBody;

    public XRBaseInteractor interactor;

    public Vector3 validPositionFound;

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

        if (rigidBody.velocity.magnitude < 0.01f) {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.06f)) {
            if (LookForPosition()) {
                isActive = true;
                rigidBody.velocity = Vector3.zero;
            }
        } else {
            isActive = false;
        }
    }

    bool LookForPosition() {
        
        int tries = 10;
        bool found = false;
        Vector2 offset = Vector2.zero;
        Collider[] colliders;
        Vector3 capsulePoint;

        while (tries > 0 && !found) {
            tries -= 1;
            found = true;
            capsulePoint = transform.position + new Vector3(offset.x, 0.5f, offset.y);

            colliders = Physics.OverlapCapsule(capsulePoint, capsulePoint + Vector3.up * 1f, 0.3f);
            foreach(Collider col in colliders) {
                if (!col.CompareTag("TeleportSphere")) {
                    found = false;
                    break;
                }
            }
            if (found) {
                validPositionFound = transform.position + new Vector3(offset.x, 0.0f, offset.y);
            }
            offset = Random.insideUnitCircle * 0.5f;
        }

        return found;

    }

}
