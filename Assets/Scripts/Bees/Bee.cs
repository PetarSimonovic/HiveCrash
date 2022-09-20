using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee
{

    private string name; 
    
    private string hiveId;

    private bool isHungry = false;

    private bool inHive;

    private int pollen;

    private int pollenCollectionRate = 1;

    private int appetite = 1000;

    private float hungerInterval = 60f;

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

    public void AddPollen()
    {
      pollen += pollenCollectionRate;
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

    public void SetHunger(bool hunger)
    {
      this.isHungry = hunger;
    }

    public int GetAppetite()
    {
      return this.appetite;
    }

}
