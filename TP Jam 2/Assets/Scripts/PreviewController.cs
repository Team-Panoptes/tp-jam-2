using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PreviewController : MonoBehaviour
{

    SphereManager sphereManager;

    Material material;

    public float openedAperture = 1.5f;
    public float closedAperture = 20f;
    public float transitionTime = 1f;

    public bool opened = false;
    private float startTime;

    [SerializeField]
    private float t;
    [SerializeField]
    private float startingAperture;
    [SerializeField]
    private float targetAperture;
    [SerializeField]
    private float currentDuration;
    [SerializeField]
    private float currentAperture;
    private Image image;
    private CanvasRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        sphereManager = GameObject.FindWithTag("TeleportSphere").GetComponent<SphereManager>();
        image = GetComponent<Image>();
        currentAperture = closedAperture;
        targetAperture = closedAperture;
        renderer = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canSee = !sphereManager.interactable.isSelected;
        if (canSee != opened) {
            targetAperture = canSee ? openedAperture : closedAperture;
            startingAperture = currentAperture;
            currentDuration = transitionTime * Mathf.Abs(targetAperture - startingAperture) / Mathf.Abs(closedAperture - openedAperture);
            opened = canSee;
            t = Time.time;
        }
        if (currentAperture != targetAperture) {
            currentAperture = Mathf.SmoothStep(startingAperture, targetAperture, Mathf.Clamp01((Time.time - t) / currentDuration));
            //material = renderer.GetMaterial();
            //material.SetFloat("_Aperture", currentAperture);
            renderer.GetMaterial().SetFloat("_Aperture", currentAperture);
            //renderer.SetMaterial(material, 0);
        }
    }
}
