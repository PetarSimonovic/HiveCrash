using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Hive hive;
    private Bee bee;
    private Timer timer;

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
          else if (bee.IsHungry())
          {
             beeEat(bee);
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
      }
    }

    private void checkBeeIsEnteringHive(Bee bee, GameObject beeBody)
    {
      if (beeBody.GetComponent<BeeBody>().isEnteringHive)
      {
          hive.SetPollen(bee.GetPollen());
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

    private void beeEat(Bee bee)
    {
      int hivePollen = hive.GetPollen();
      {
        if (hivePollen >= bee.GetAppetite())
        {
          hive.SetPollen(-bee.GetAppetite());
          Debug.Log("bee has eaten - hive pollen now " + hive.GetPollen());
          bee.ResetTimer();
        }
        else 
        {
          Debug.Log("Not enough food");
          bee.ResetTimer();
        }
        bee.SetHunger(false);
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
        bee.ResetTimer();
        hive.AddBee(bee);
      }
    }

    private Timer initaliseTimer()
    {
      var timer = gameObject.GetComponent<Timer>();
      return timer;
    }
}
