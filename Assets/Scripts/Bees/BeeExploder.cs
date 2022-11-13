using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BeeExploder : MonoBehaviour
{

[SerializeField]
GameObject beeAnatomyHexBlack;

    public void explodeBee(Vector3 position) 
    {   
        Debug.Log("exploding bee at " + position);
    }


}
