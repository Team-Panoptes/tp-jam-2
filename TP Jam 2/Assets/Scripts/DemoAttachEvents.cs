using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAttachEvents : MonoBehaviour
{

    public void OnAttach() {
        Debug.Log("Attached!", gameObject);
    }

    public void OnDetach() {
        Debug.Log("Detached!", gameObject);
    }

}
