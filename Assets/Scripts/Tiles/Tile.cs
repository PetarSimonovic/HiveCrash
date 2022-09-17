using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private float[] heightRange = new float[2];

    private GameObject hex;

    private bool isHidden = false;

    private MeshRenderer renderer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
      hex = this.gameObject.transform.GetChild(0).gameObject;
      renderer = hex.GetComponent<MeshRenderer>();
      isHidden = true;
      this.gameObject.name = material.ToString();
    }

    public void OnTriggerEnter(Collider collision)
    {
      checkCollision(collision);
    }

    public void OnCollisionEnter(Collision collision)
    {
       checkCollision(collision.collider);
    }

    public void checkCollision(Collider other)
    {
      if (isHidden) 
      {
       reveal();
      }

    }

    private void reveal()
    {
      renderer.material = material;
      transform.position = new Vector3 (transform.position.x, Random.Range(heightRange[0], heightRange[1]), transform.position.z);
      isHidden = false;
    }

    public bool IsHidden()
    {
      return isHidden;
    }


}
