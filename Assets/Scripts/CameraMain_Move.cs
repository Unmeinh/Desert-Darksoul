using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain_Move : MonoBehaviour
{
    public Transform trackingTarget;
    public AudioSource auSrc;

    void Start()
    {
        transform.position = new Vector3(-5, -1, -10);

        if (PlayerPrefs.HasKey("SrcVol"))
        {
            if (auSrc != null)
            {
                auSrc.volume = PlayerPrefs.GetFloat("SrcVol");
            }
        }
    }

    void Update()
    {
        MovingMainCamera();
    }

    void MovingMainCamera()
    {
        float y = trackingTarget.position.y + 2.022404F;
        if (y >= 1)
        {
            if (trackingTarget.position.x > -5)
            {
                transform.position = new Vector3(
                    trackingTarget.position.x,
                    transform.position.y,
                    transform.position.z
                );
            }
            else
            {
                transform.position = new Vector3(-5, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (trackingTarget.position.x > -5)
            {
                if (y > -1)
                {
                    transform.position = new Vector3(
                        trackingTarget.position.x,
                        y,
                        transform.position.z
                    );
                }
                else
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        -1,
                        transform.position.z
                    );
                }
            }
            else
            {
                transform.position = new Vector3(-5, y, transform.position.z);
            }
        }
    }
}
