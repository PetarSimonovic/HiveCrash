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
      checkBees();
      removeDeadBees();
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
             eat(bee);
          }
          checkBeeTimer(bee);
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

    private void eat(Bee bee)
    {
      int hivePollen = hive.GetPollen();
      Debug.Log("HivePollen " + hivePollen);
      if (hivePollen >= bee.GetAppetite())
      {
        hive.SetPollen(-bee.GetAppetite());
        bee.SetMessage(bee.GetName() + " has eaten");
        bee.IncreaseHealth();
      }
      else 
      {
        bee.SetMessage("Not enough food for " + bee.GetName());
        bee.ReduceHealth();
      }
      Debug.Log(bee.GetName() + " health: " + bee.GetHealth());
      bee.SetHunger(false);
      bee.RestartHungerTimer();
    }

    private void checkBeeHealth(Bee bee)
    {
      deadBees = new List<Bee>();
      if (bee.GetHealth() < 0)
      {
        deadBees.Add(bee);
      }
    }

    private void removeDeadBees()
    {
      foreach (Bee bee in deadBees)
      {
        hive.RemoveBee(bee);
      }
      deadBees.Clear();
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
    }

    public void AddBees(int beeCount)
    {
      for (int i = 0; i < beeCount; i++)
      {
        Timer timer = initaliseTimer();
        string name = beeNamer.ChooseName();
        Bee bee =  new Bee(name, this.hive.GetId(), timer);
        bee.SetMessage(name + " hatched");
        hive.AddBee(bee);
      }
    }

    private Timer initaliseTimer()
    {
      Timer timer = gameObject.GetComponent<Timer>();
      return timer;
    }
}
