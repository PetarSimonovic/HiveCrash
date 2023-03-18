using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticTextBubble : MonoBehaviour
{
    private TMP_Text uiText;

    private Color32 colour = new Color32(200, 200, 200, 255);



    

    void Awake()
    {
        getComponents();
        uiText.GetComponent<TMP_Text>().fontSize = 35;


    }

    // Update is called once per frame
  


    public void SetText(string text)
    {
        uiText.color = colour;
        uiText.SetText(text);
    }


    private void getComponents() 
    {
        uiText  = GetComponent<TMP_Text>();
    }

    public void SetColour(Color32 colour) 
    {
        this.colour = colour;
    }
}
