using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMap_Move : MonoBehaviour
{
    public Transform trackingTarget;

    void Start()
    {
        transform.position = new Vector3(-2.17F, 0, -10);
    }

    void Update()
    {
        MovingMapCamera();
    }

    void MovingMapCamera()
    {
        if (trackingTarget.position.x > -2.17F)
        {
            transform.position = new Vector3(trackingTarget.position.x, 0, -10);
        }
        else
        {
            transform.position = new Vector3(-2.17F, 0, -10);
        }
    }

}
