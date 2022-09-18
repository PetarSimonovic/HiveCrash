using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee
{
    // Start is called before the first frame update

    private string hiveId;

    private bool inHive;

    private int pollen;

    private int pollenCollectionRate = 1;

    private GameObject body;

    public Bee(string hiveId)
    {
      this.hiveId = hiveId;
      this.inHive = true;
    }

    public string GetHiveId()
    {
      return this.hiveId;
    }

    public bool IsInHive()
    {
      return this.inHive;
    }

    public void Fly()
    {
      this.inHive = false;
    }

    public void EnterHive()
    {
      this.inHive = true;
    }

    public void SetBody(GameObject body)
    {
      this.body = body;
    }

    public GameObject GetBody()
    {
      return this.body;
    }

    public void AddPollen()
    {
      pollen += pollenCollectionRate;
    }

    public int GetPollen()
    {
      return pollen;
    }

    public void RemoveAllPollen()
    {
      this.pollen = 0;
      Debug.Log("Bee pollen is " + this.pollen);
    }
}
