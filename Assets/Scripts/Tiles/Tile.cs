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

    protected bool isHidden = true; // why does this have to be false then immediately set to true in start?

    private MeshRenderer mesh;
    // Start is called before the first frame update

    protected virtual void  Awake()
    {
      hex = this.gameObject.transform.GetChild(0).gameObject;
      mesh = hex.GetComponent<MeshRenderer>();
    }

    protected virtual void Start()
    {
      this.gameObject.name = material.ToString();
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
      checkCollision(collision);
    }

    public void checkCollision(Collider other)
    {
      if (isHidden) 
      {
       reveal();
      }

    }

    protected virtual void reveal()
    {
      mesh.material = material;
      transform.position = new Vector3 (transform.position.x, Random.Range(heightRange[0], heightRange[1]), transform.position.z);
      isHidden = false;
    }

    public bool IsHidden()
    {
      return isHidden;
    }

    public float GetHeight()
    {
      Vector3 bounds = this.mesh.bounds.size;
      Debug.Log(bounds);
      return bounds.z;
    }

    public void GetDebugData() 
    {
      Debug.Log("Enemy tile here");
      Debug.Log(isHidden);
      Debug.Log(this.gameObject.name = material.ToString());
    }


}
