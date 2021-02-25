using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{

    public float speed;
    public Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5f, 45f);
        axis = Random.insideUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, axis, speed * Time.deltaTime);
    }
}
