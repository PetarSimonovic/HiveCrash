using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    private TMP_Text uiText;

    private Timer timer;

    private float countDownSeconds = 10.0f;

    private float riseSpeed = 0.008f;

    private Color32 hiveCrashYellow = new Color32(233, 196, 106, 255);

    private Color32 hiveCrashRed = new Color32(227, 12, 14, 255);
    

    void Awake()
    {
        getComponents();
        uiText.overrideColorTags = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfTimerIsUp();
        transform.position += new Vector3(0, riseSpeed, 0);

    }



    public void SetText(string text, bool isPositive)
    {
        switch (isPositive) 
        {
            case true:
                uiText.color = hiveCrashYellow;
                break;
            
            case false:
                uiText.color = hiveCrashRed;
                uiText.faceColor = hiveCrashRed;
                uiText.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, hiveCrashRed);

                break;
        }
        uiText.SetText(text);
        initaliseTimer();
    }

    private void initaliseTimer() 
    {
        timer.SetCountdownSeconds(countDownSeconds);
        timer.Restart();
        timer.SetOn(true);
    }

    private void getComponents() 
    {
        timer   = GetComponent<Timer>();
        uiText  = GetComponent<TMP_Text>();
    }

    private void checkIfTimerIsUp()
    {
        if (!timer.IsOn()) 
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        var magnitude = 20;

        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (0f, 0f, Random.Range(-20.0f, 20.0f)));
    }
}
