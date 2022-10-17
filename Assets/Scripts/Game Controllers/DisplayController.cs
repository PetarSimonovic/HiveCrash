using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject hivePollenCount;

    [SerializeField]
    private GameObject enemyPollenCount;

    [SerializeField]
    private GameObject beeCount;

    [SerializeField]
    private GameObject infoPane;

    private Hive hive;

    private Hive enemyHive;

    private bool gameStarted = false;

    private bool enemyDiscovered = false;



    void Update()
    {
        if (gameStarted)
        {
            hivePollenCount.GetComponent<UIText>().SetText(hive.GetPollenPercentage() + "%");
            beeCount.GetComponent<UIText>().SetText(hive.GetBees().Count.ToString());
            infoPane.GetComponent<UIText>().SetText(getBeeInfo());
        }
        if (enemyDiscovered)
        {
            enemyPollenCount.GetComponent<UIText>().SetText(enemyHive.GetPollenPercentage() + "%");
        }
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
        this.gameStarted = true;
    }

      public void SetEnemyHive(Hive enemyHive)
    {
        this.enemyHive = enemyHive;
        this.enemyDiscovered = true;
    }

    private string getBeeInfo()
    {
        string beeInfo = "";

        foreach (Bee bee in hive.GetBees())
        {
            beeInfo = (beeInfo + bee.GetMessage() +  Environment.NewLine);
        }

        return beeInfo;
    } 

}
