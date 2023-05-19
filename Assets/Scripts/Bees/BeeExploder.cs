using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BeeExploder : MonoBehaviour
{


private Vector3 position;

private List<GameObject> beeParts = new List<GameObject>();

private float forceMultiplier = 1.5f;

private Timer timer;

private bool timing;

private bool isPlayer;


    private void Update() {
      // if (!timer.IsOn() && beeParts.Count > 0) {destroyBeeParts();}
    }

    public void explodeBee(Transform bee, Vector3 position, bool isPlayer) 
    {   
        beeParts.Clear();
        initialiseTimer();
        this.position = position;
        this.isPlayer = isPlayer;
        addBeeParts(bee);
        addForceToBeeParts();
        
    }

    private void initialiseTimer()
    {
        timer = gameObject.GetComponent<Timer>();
        timer.SetOn(true);
        timer.SetCountdownSeconds(10f);
    }

    private void addBeeParts(Transform bee) {

        foreach (Transform child in bee) {
            Debug.Log(child.gameObject.name);
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Rigidbody rigidBody = child.gameObject.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            rigidBody.mass = 0.35f;
            GameObject part = Instantiate(child.gameObject, position, Quaternion.identity);
            beeParts.Add(part);
        }    
            Debug.Log("Bee parts");
                foreach (GameObject beePart in beeParts) {
                    Debug.Log(beePart);
                }
            
    }


    private GameObject instantiateBeePart(GameObject part) {

      return Instantiate(part, position, Quaternion.identity);

    }

    private void addForceToBeeParts() {
        foreach (GameObject beePart in beeParts) {
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

    private void destroyBeeParts() {
        foreach (GameObject beePart in beeParts) {
            Destroy(beePart);
        }
        beeParts.Clear();
    }


}
