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

    private bool nextToHive;

    private MapCreator mapCreator;

    protected bool isHidden = true; // why does this have to be false then immediately set to true in start?

    private MeshRenderer mesh;

    public int row;

    public int column;

    public bool isBorderTile;

    protected virtual void  Awake()
    {
      hex = this.gameObject.transform.GetChild(0).gameObject;
      mesh = hex.GetComponent<MeshRenderer>();
    }

    protected virtual void Start()
    {
      this.gameObject.name = material.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
      checkCollision(other);
    }

    public void checkCollision(Collider other)
    {
      if (other.gameObject.tag == "hive" && !nextToHive && isHidden)
      {
        nextToHive = true;
        Debug.Log("next to hive!");
        mapCreator.ChangeTileToMeadow(this.transform.position);
        return;
       // Reveal();
      }
      if (isHidden) 
      {
        Reveal();
      }

    }

    public virtual void Reveal()
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
      return bounds.z;
    }

    public void GetDebugData() 
    {
      Debug.Log(this.gameObject.name = material.ToString());
    }

    public void PrintPosition()
    {
      Debug.Log("Column: " + column + "| Row:  " + row);
    }

    public void SetBorderTile()
    {
      isBorderTile = true;
    }

    public bool IsBorderTile() 
    {
      return isBorderTile;
    }

    public bool IsNextToHive() 
    {
      return nextToHive;
    }

    public void SetMapCreator(MapCreator mapCreator) {
      this.mapCreator = mapCreator;
    }


}
