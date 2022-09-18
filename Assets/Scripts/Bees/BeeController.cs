using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Hive hive;
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
          bee.EnterHive();
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
}
