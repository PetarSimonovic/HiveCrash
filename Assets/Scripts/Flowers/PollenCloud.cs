using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCloud : MonoBehaviour
{
    [SerializeField]
    GameObject flower;
    private float rotationSpeed = 50f;
    private float maxRotationSpeed = 960;

    private bool hasBeenShed = false;
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

    public void Pollenate(Vector3 position)
    {
        Debug.Log("Pollenating!");
        hasBeenShed = true;
    }

    public bool HasBeenShed()
    {
        return this.hasBeenShed;
    }
}
