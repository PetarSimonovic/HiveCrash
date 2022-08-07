using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee
{
    // Start is called before the first frame update

    private int hiveId;

    public Bee(int hiveId)
    {
      this.hiveId = hiveId;
    }

    public int GetHiveId()
    {
      return this.hiveId;
    }
}
