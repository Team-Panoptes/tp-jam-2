using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{

    public bool followPosition;
    public bool followRotation;
    public Transform positionTarget;
    public Transform rotationTarget;
    public Vector3 positionOffset;


    // Update is called once per frame
    void LateUpdate()
    {
        if (followPosition) {
            transform.position = positionTarget.position + positionOffset;
        }
        if (followRotation) {
            transform.rotation = rotationTarget.rotation;
        }
    }
}
