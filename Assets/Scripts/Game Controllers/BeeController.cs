using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Hive hive;
    private Bee bee;
    private BeeNamer beeNamer = new BeeNamer();
    private List<Bee> deadBees = new List<Bee>();

    private void Update()
    {
     // checkBees();
      removeDeadBees();
    }


    private void checkBees()
      {
        foreach (Bee bee in hive.GetBees())
        {
          if (!bee.IsInHive())
          {
            GameObject beeBody = bee.GetBody();
         //   checkBeeIsEnteringHive(bee, beeBody);
         //   checkPollenCollection(bee, beeBody);
          }
          else if (bee.IsHungry())
          {
            eat(bee);
          }
         // checkBeeTimer(bee);
          checkBeeHealth(bee);
        }
      }

    private void checkBeeTimer(Bee bee)
    {    
      if (bee.GetTimer().GetTime() < 1)
      {
        bee.SetHunger(true);
      }
    }



    private void checkPollenCollection(Bee bee, GameObject beeBody)
    {
      if (beeBody.GetComponent<BeeBody>().CollectingPollen())
      {
        bee.CollectPollen();
      }
    }

    private void eat(Bee bee)
    {
      int hivePollen = hive.GetPollen();
      if (hivePollen >= bee.GetAppetite())
      {
        hive.RemovePollen(bee.GetAppetite());
        string message = bee.GetName() + " ate: -" + bee.GetAppetite() + " pollen";
        hive.LaunchTextBubble(message, false);
        bee.IncreaseHealth();
      }
      else 
      {
        bee.ReduceHealth();
        string message = bee.GetHealth() <= 0 ? bee.GetName() + " died of hunger" : "No food for " + bee.GetName();
        hive.LaunchTextBubble(message, false);
      }
      bee.SetHunger(false);
      bee.RestartHungerTimer();
    }

    private void checkBeeHealth(Bee bee)
    {
      if (bee.GetHealth() <= 0)
      {
        deadBees.Add(bee);
      }
    }

    private void removeDeadBees()
    {
      foreach (Bee bee in deadBees)
      {
        hive.RemoveBee(bee);
        Destroy(bee.GetBody());
      }
      deadBees.Clear();
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
      hive.LaunchTextBubble("Hive built");

    }

    public void AddBees(int beeCount)
    {
      for (int i = 0; i < beeCount; i++)
      {
        hive.AddBee();
      }
    }

    private Timer initaliseTimer()
    {
      Timer timer = gameObject.GetComponent<Timer>();
      return timer;
    }
}
