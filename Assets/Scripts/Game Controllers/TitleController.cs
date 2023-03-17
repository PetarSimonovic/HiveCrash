using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{

    private bool playerIsReady = false;



     private CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraController(CameraController cameraController) 
    {
        this.cameraController = cameraController;
    }

    public bool IsPlayerReady()
    {
        return playerIsReady;
    }


}
