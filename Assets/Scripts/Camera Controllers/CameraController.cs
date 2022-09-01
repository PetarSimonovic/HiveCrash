using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 centreTile = new Vector3(2.00f, 0.00f, 2.00f); 
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f);
    transform.LookAt(centreTile);
    }


}
