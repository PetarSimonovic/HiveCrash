using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Hive hive;
    private Bee bee;
    private Timer timer;
    private float beeHungerInterval = 60f;

    private void Update()
    {
      checkBees();
    }



    private void checkBees()
      {
        foreach (Bee bee in hive.GetBees())
        {
          if (!bee.IsInHive())
          {
            GameObject beeBody = bee.GetBody();
            checkBeeIsEnteringHive(bee, beeBody);
            checkPollenCollection(bee, beeBody);
          }
          checkBeeTimer(bee);
        }
      }

    private void checkBeeTimer(Bee bee)
    {
      var timer = bee.GetTimer();
      if (timer.GetTime() <= 1)
      {
        bee.SetHunger(true);
        timer.SetTime(beeHungerInterval);
        Debug.Log(bee.IsHungry());
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
        timer = initaliseTimer();
        var bee =  new Bee(this.hive.GetId());
        bee.SetTimer(timer);
        hive.AddBee(bee);
      }
    }

    private Timer initaliseTimer()
    {
      var timer = gameObject.GetComponent<Timer>();
      timer.SetTime(beeHungerInterval);
      return timer;
    }
}
