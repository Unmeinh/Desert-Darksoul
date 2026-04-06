using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Running : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * 0.15f, 0f);
    }
}
