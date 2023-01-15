using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    private Vector3 isometricRotation = new Vector3(30, 45, 0);
    private Vector3 hiveCrashRotation = new Vector3(30, 0, 0);
    private Vector3 hiveCrashPosition = new Vector3(3.5f, 1f, 3f);


    public void Awake()
    {
      transform.eulerAngles = isometricRotation;
      transform.position = hiveCrashPosition;
    }
}
