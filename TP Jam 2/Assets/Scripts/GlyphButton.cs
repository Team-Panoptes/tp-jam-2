using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class GlyphButton : MonoBehaviour
{

    public UnityEvent onPressed;

    public Animator animator;
    public TextMeshProUGUI text;

    private GameManager manager;
    public bool dontAlertManager;

    void Awake() {
        if (!dontAlertManager) {
            manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public void Press(HoverEnterEventArgs hoverArgs) {
        animator.SetTrigger("Press");
        onPressed?.Invoke();
        if(manager != null) {
            manager.EnterCode(text.text);
        }
        HoverExitEventArgs args = new HoverExitEventArgs();
        args.interactable = hoverArgs.interactable;
        args.interactor = hoverArgs.interactor;
        args.isCanceled = false;
        StartCoroutine(CancelHover(hoverArgs.interactor, args));
    }

    IEnumerator CancelHover(XRBaseInteractor interactor, HoverExitEventArgs args) {
        yield return null;

        interactor.hoverExited?.Invoke(args);

    }



}
