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
        Debug.Log("Processing touch");
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
      Debug.Log(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
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
      Debug.Log(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
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
        Debug.Log("RotateUp");
        float xRotation = 0.1f;
        transform.Rotate(xRotation, 0.0f, 0.0f);
        camera.transform.LookAt(centreTile);
    }

    private void rotateDown()
    {
        Debug.Log("RotateDown");
        float xRotation = -0.1f;
        transform.Rotate(xRotation, 0.0f, 0.0f);
        camera.transform.LookAt(centreTile);
    }

     private void rotateLeft()
    {
        Debug.Log("RotateLeft");
        float yRotation = 0.1f;
        transform.Rotate(0.0f, yRotation, 0.0f);
        camera.transform.LookAt(centreTile);
    }

    private void rotateRight()
    {
        Debug.Log("RotatRight");
        float yRotation = -0.1f;
        transform.Rotate(0.0f, yRotation, 0.0f);
        camera.transform.LookAt(centreTile);
    }

}
