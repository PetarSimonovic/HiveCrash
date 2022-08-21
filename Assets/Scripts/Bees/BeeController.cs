using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    // Start is called before the first frame update
    public void CheckBees(List<Bee> bees)
      {
        foreach (Bee bee in bees)
        {
          if (!bee.IsInHive())
          {
            GameObject beeBody = bee.GetBody();
            checkBeeIsReturningToHive(bee, beeBody);
          }
        }
      }

    private void checkBeeIsReturningToHive(Bee bee, GameObject beeBody)
    {
      if (beeBody.GetComponent<BeeBody>().isEnteringHive)
      {
          bee.Reset();
          Destroy(beeBody);
      }
    }
}
