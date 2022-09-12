using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimator : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
     animator = GetComponent<Animator>();
     Debug.Log(animator);
     Debug.Log(animator.enabled);
     Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
    }

    public void Bloom()
    {
     Debug.Log("In bloom on flowerAnimator");
     GetComponent<Animator>().SetBool("InBloom", true);
    }

    public void Close()
    {
        Debug.Log("In close on flowerAnimator");
        GetComponent<Animator>().SetBool("InBloom", false);
    }
}
