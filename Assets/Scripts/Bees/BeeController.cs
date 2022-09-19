using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Hive hive;

    private Bee bee;
    // Start is called before the first frame update
    public void CheckBees(List<Bee> bees)
      {
        foreach (Bee bee in bees)
        {
          if (!bee.IsInHive())
          {
            GameObject beeBody = bee.GetBody();
            checkBeeIsEnteringHive(bee, beeBody);
            checkPollenCollection(bee, beeBody);
          }
        }
      }

    private void checkBeeIsEnteringHive(Bee bee, GameObject beeBody)
    {
      if (beeBody.GetComponent<BeeBody>().isEnteringHive)
      {
          hive.AddPollen(bee.GetPollen());
          bee.EnterHive();
          bee.RemoveAllPollen();
          Destroy(beeBody);
      }
    }

    private void checkPollenCollection(Bee bee, GameObject beeBody)
    {
      if (beeBody.GetComponent<BeeBody>().CollectingPollen())
      {
        bee.AddPollen();
      }
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
    }

    public void AddBees(int beeCount)
    {
      for (int i = 0; i < beeCount; i++)
      {
        var bee =  new Bee(this.hive.GetId());
        hive.AddBee(bee);
      }
    }
}
