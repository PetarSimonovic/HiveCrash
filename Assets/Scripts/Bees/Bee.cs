using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee 
{

    private string name; 

    private int health = 3;

    private string hiveId;

    private bool isHungry = false;

    private bool inHive;

    private int pollen;

    private int pollenCollectionRate = 1;

    private int appetite = 1000;

    private float hungerInterval = 60f;

    private string message;

    private Timer timer;

    private GameObject body;

    public Bee(string hiveId)
    {
      this.hiveId = hiveId;
      this.inHive = true;
    }

    public Bee(string name, string hiveId, Timer timer)
    {
      this.name = name;
      this.hiveId = hiveId;
      this.inHive = true;
      this.timer = timer;
      timer.SetCountdownSeconds(this.hungerInterval);
      timer.Restart();
    }

    public Timer GetTimer()
    {
      return this.timer;
    }


    public string GetHiveId()
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

    public void EnterHive()
    {
      this.inHive = true;
      
    }

    public void SetBody(GameObject body)
    {
      this.body = body;
    }

    public GameObject GetBody()
    {
      return this.body;
    }

    public void CollectPollen()
    {
      pollen += pollenCollectionRate;
    }

    public void AddPollen(int pollen)
    {
      this.pollen += pollen;
    }

    public int GetPollen()
    {
      return pollen;
    }

    public void RemoveAllPollen()
    {
      this.pollen = 0;
    }

    public bool IsHungry()
    {
      return this.isHungry;
    }

    public void RestartHungerTimer()
    {
      this.timer.Restart();
    }

    public void SetHunger(bool hunger)
    {
      this.isHungry = hunger;
    }

    public int GetAppetite()
    {
      return this.appetite;
    }

    public void ReduceHealth()
    {
      this.health -= 1;
    }

    public void IncreaseHealth()
    {
      this.health += 1;
    }

    public string GetName()
    {
      return this.name;
    }

    public int GetHealth()
    {
      return this.health;
    }

    public void SetMessage(string message)
    {
      this.message = message;
    }

    public string GetMessage()
    {
      return this.message;
    }

    public void SetPollenCollectionRate(int pollenCollectionRate)
    {
      this.pollenCollectionRate = pollenCollectionRate;
    }

    public void SetHealth(int health)
    {
      this.health = health;
    }

}
