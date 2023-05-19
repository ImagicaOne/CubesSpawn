using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var randomColor = Random.ColorHSV(0.1f, 0.9f, 0.9f, 1f, 1f, 1f, 1f, 1f);
        GetComponent<Renderer>().material.color = randomColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
