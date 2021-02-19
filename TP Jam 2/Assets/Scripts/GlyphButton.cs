using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlyphButton : MonoBehaviour
{

    public UnityEvent onPressed;

    public Animator animator;

    public void Press() {
        animator.SetTrigger("Press");
        onPressed?.Invoke();
    }

}
