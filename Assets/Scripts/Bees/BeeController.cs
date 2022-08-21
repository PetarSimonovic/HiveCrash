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
            if (beeBody.GetComponent<BeeBody>().isEnteringHive)
            {
                bee.Reset();
                Destroy(beeBody);
            }
          }
        }
      }
}
