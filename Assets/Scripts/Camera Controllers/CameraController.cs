using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    [SerializeField]
    private Vector3 centreTile = new Vector3(2.00f, 0.00f, 2.00f);

    [SerializeField]
    Camera camera;

    private Vector3 startTouchPosition;

    private Vector3 touchPosition;
    
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = centreTile;
        camera.transform.LookAt(centreTile);
    }

    public void ProcessTouch(Vector3 touchPosition)
    {
        settouchPosition(touchPosition);
        processZAxisRotation();
        processYAxisRotation();
    }


    private void settouchPosition(Vector3 touchPosition)
    {
        this.touchPosition = touchPosition;
    }

    public void SetStartTouchPosition(Vector3 touchPosition)
    {
        this.startTouchPosition = touchPosition;
    }


    private void processZAxisRotation()
    {
      if (this.touchPosition.y < this.startTouchPosition.y)
      {
        rotateUp();
      } 
      else if (this.touchPosition.y > this.startTouchPosition.y)
      {
        rotateDown();
      }
        
    }


    private void processYAxisRotation()
    {
      if (this.touchPosition.x < this.startTouchPosition.x)
      {
        rotateLeft();
      } 
      else if (this.touchPosition.x > this.startTouchPosition.x)
      {
        rotateRight();
      }
        
    }

    private void rotateUp()
    {
        float xRotation = 0.1f;
        transform.Rotate(xRotation, 0.0f, 0.0f);
        camera.transform.LookAt(centreTile);
    }

    private void rotateDown()
    {
        float xRotation = -0.1f;
        transform.Rotate(xRotation, 0.0f, 0.0f);
        camera.transform.LookAt(centreTile);
    }

     private void rotateLeft()
    {
        float yRotation = 0.1f;
        transform.Rotate(0.0f, yRotation, 0.0f);
        camera.transform.LookAt(centreTile);
    }

    private void rotateRight()
    {
        float yRotation = -0.1f;
        transform.Rotate(0.0f, yRotation, 0.0f);
        camera.transform.LookAt(centreTile);
    }

}