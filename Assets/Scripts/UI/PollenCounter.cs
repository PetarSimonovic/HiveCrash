using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PollenCounter : MonoBehaviour
{
    private TMP_Text uiText;



    

    void Awake()
    {
        getComponents();
        uiText.color = new Color32(200, 200, 200, 255);
        uiText.GetComponent<TMP_Text>().fontSize = 35;


    }

    // Update is called once per frame
  


    public void SetPollenCount(int pollenCount, int beesInHive, int bees)
    {
        
        uiText.SetText(beesInHive + "/" + bees + " " + pollenCount.ToString() + "%");
    }


    private void getComponents() 
    {
        uiText  = GetComponent<TMP_Text>();
    }
}
