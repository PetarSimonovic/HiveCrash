using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee
{
    // Start is called before the first frame update

    private int hiveId;
    private bool inHive;

    public Bee(int hiveId)
    {
      this.hiveId = hiveId;
      this.inHive = true;
    }

    public int GetHiveId()
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
}
