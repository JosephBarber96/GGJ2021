using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{

    public bool Rotate = false;
    public float RotationSpeed = 1.0f;

    public float outerRadiusMax = 6;
    public float outerRAdiusMin = 5;

    // Update is called once per frame
    void Update()
    {
        if (Rotate)
        {
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
