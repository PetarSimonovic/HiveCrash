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
    }

    public void Bloom()
    {
     Debug.Log("In bloom on flowerAnimator");
     animator.Play("Base Layer.OpenFlower", 0, 0.25f);
    }

    public void Close()
    {
        Debug.Log("In close on flowerAnimator");
        animator.Play("Base Layer.CloseFlower", 0, 0.25f);

    }
}
