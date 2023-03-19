using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject staticTextBubblePrefab;

    GameObject hiveCrashTitle;

    private bool playerIsReady = false;



    // Start is called before the first frame update
    void Start()
    {        

    }

    public void CreateTitles() 
    {
        createTitleText();
    }

    public void RemoveTitles()
    {
        Destroy(hiveCrashTitle);
    }


    // Update is called once per frame
    void Update()
    {

    }

   
    

    private void createTitleText()
    {
        hiveCrashTitle = Instantiate(staticTextBubblePrefab);
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetText("HiveCrash");
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetSize(90);
        hiveCrashTitle.transform.position = new Vector3(Globals.centreTile.x, 2f, Globals.centreTile.z);

    }





}
