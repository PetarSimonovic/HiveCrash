using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GameController gameController;

    void Awake() 
    {
        Application.targetFrameRate = 60;

    }
    void Start()
    {
        gameController = Instantiate(gameController);
        gameController.SetCameraController(this.cameraController);
        gameController.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
