using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCloud : MonoBehaviour
{

    private float rotationSpeed = 50f;
    private float maxRotationSpeed = 960;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime, 0); 
        if (rotationSpeed < maxRotationSpeed) 
        {
            rotationSpeed += 1f;
        }
    }
}
