using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    private Vector3 isometricRotation = new Vector3(30, 45, 0);
    private Vector3 hiveCrashRotation = new Vector3(30, 0, 0);
    private Vector3 hiveCrashPosition = new Vector3(2, 5, 0);


    public void Awake()
    {
      transform.eulerAngles = hiveCrashRotation;
      transform.position = hiveCrashPosition;
    }
}