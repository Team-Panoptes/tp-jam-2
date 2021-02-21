using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GlyphButton : MonoBehaviour
{

    public UnityEvent onPressed;

    public Animator animator;
    public TextMeshProUGUI text;

    private GameManager manager;
    public bool dontAlertManager;

    void Awake() {
        if (dontAlertManager) {
            manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public void Press() {
        animator.SetTrigger("Press");
        onPressed?.Invoke();
        if(manager != null) {
            manager.EnterCode(text.text);
        }
    }




}
