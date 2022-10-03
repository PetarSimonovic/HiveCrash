using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject hivePollenCount;

    [SerializeField]
    private GameObject beeCount;

    [SerializeField]
    private GameObject infoPane;

    private Hive hive;

    private bool gameStarted = false;



    void Update()
    {
        if (gameStarted)
        {
            hivePollenCount.GetComponent<UIText>().SetText(hive.GetPollen().ToString());
            beeCount.GetComponent<UIText>().SetText(hive.GetBees().Count.ToString());
            infoPane.GetComponent<UIText>().SetText(getBeeInfo());
        }
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
        this.gameStarted = true;
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
