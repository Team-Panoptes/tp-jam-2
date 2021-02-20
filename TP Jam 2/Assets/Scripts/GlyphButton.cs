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

    void Awake() {
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Press() {
        animator.SetTrigger("Press");
        onPressed?.Invoke();
        manager.EnterCode(text.text);
    }




}
