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

     [SerializeField]
    private TitleController titleController;

    private bool gameIsPlaying;

    private bool showTitle = true;

    void Awake() 
    {
        Application.targetFrameRate = 60;

    }
    void Start()
    {
        gameController = Instantiate(gameController);
        titleController = Instantiate(titleController);
        gameController.SetCameraController(this.cameraController);
        titleController.SetCameraController(this.cameraController);
    }

    // Update is called once per frame

    public void StartGame() 
    {
        gameIsPlaying = true;
    }
    void Update()
    {

        if (showTitle) 
        {
            titleController.IsPlayerReady();
        }
        if (!gameIsPlaying) {
            gameIsPlaying = true;
            gameController.StartGame();}
    }
}
