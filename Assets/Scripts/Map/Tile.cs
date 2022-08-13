using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void DisperseFog()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
      Debug.Log(transform.position);
    }

    public void OnTriggerEnter(Collider collision)
    {
      Debug.Log(transform.position);
    }
}
