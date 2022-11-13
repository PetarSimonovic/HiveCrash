using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BeeExploder : MonoBehaviour
{

[SerializeField]
private GameObject beeAnatomyHexBlack;

[SerializeField]
private GameObject beeAnatomyHexYellow;

[SerializeField]
private GameObject beeAnatomyHexOrange;

[SerializeField]
private GameObject beeAnatomyTail;

[SerializeField]
private GameObject beeAnatomyHead;

private Vector3 position;

private List<GameObject> beeParts = new List<GameObject>();

private float forceMultiplier = 1.5f;

private bool isPlayer;

    public void explodeBee(Vector3 position, bool isPlayer) 
    {   
        beeParts.Clear();
        this.position = position;
        this.isPlayer = isPlayer;
        addBeeParts();
        addForceToBeeParts();
        
    }

    private void addBeeParts() {

       GameObject hexBlack = instantiateBeePart(beeAnatomyHexBlack);
       GameObject hexOne = isPlayer ? instantiateBeePart(beeAnatomyHexYellow) : instantiateBeePart(beeAnatomyHexOrange);
       GameObject hexTwo = isPlayer ? instantiateBeePart(beeAnatomyHexYellow) : instantiateBeePart(beeAnatomyHexOrange);
       GameObject tail = instantiateBeePart(beeAnatomyTail);
       GameObject head = instantiateBeePart(beeAnatomyHead); 
       beeParts.Add(hexBlack);
       beeParts.Add(hexOne);
       beeParts.Add(hexTwo);
       beeParts.Add(tail);
       beeParts.Add(head);
    }

    private GameObject instantiateBeePart(GameObject part) {

      return Instantiate(part, position, Quaternion.identity);

    }

    private void addForceToBeeParts() {
        Debug.Log("Adding Force");
        foreach (GameObject beePart in beeParts) {
            Debug.Log(beePart);
            Rigidbody rigidBody = beePart.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.up * forceMultiplier, ForceMode.Impulse);
           applyRandomDirectionalForce(rigidBody);
        }
    }

    private void applyRandomDirectionalForce(Rigidbody rigidBody) 
    {
        int direction = Random.Range(1, 5);
        switch (direction) {
            case 1:
                rigidBody.AddForce(-transform.right, ForceMode.Impulse);
                break;
            case 2: 
                rigidBody.AddForce(transform.right, ForceMode.Impulse);
                break;
            case 3: 
                rigidBody.AddForce(transform.forward, ForceMode.Impulse);
                break;
            default: 
                rigidBody.AddForce(-transform.forward, ForceMode.Impulse);
                break;


        }
    }


}
