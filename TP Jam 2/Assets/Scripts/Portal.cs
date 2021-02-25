using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public UnityEvent onSphereGoesTrough;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TeleportSphere")) {
            onSphereGoesTrough?.Invoke();
        }
    }

    public void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }


}
