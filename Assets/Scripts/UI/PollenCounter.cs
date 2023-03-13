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
    }

    // Update is called once per frame
  


    public void SetPollenCount(int pollenCount)
    {
        
        uiText.color = new Color32(255, 255, 255, 255);
        uiText.SetText(pollenCount.ToString() + "%");
    }


    private void getComponents() 
    {
        uiText  = GetComponent<TMP_Text>();
    }
}
