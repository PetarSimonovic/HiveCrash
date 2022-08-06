using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    private Vector3 isometricRotation = new Vector3(30, 45, 0);

    public void Awake()
    {
      transform.eulerAngles = isometricRotation;
    }
}
