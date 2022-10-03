using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject hivePollenCount;

    private Hive hive;

    private bool gameStarted = false;



    void Update()
    {
        if (gameStarted)
        {
            hivePollenCount.GetComponent<UIText>().SetText(hive.GetPollen().ToString());
        }
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
        this.gameStarted = true;
    }
}
