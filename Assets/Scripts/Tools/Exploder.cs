using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Exploder : MonoBehaviour
{


private Vector3 position;

private List<GameObject> parts = new List<GameObject>();

private float forceMultiplier = 1.5f;

private Timer timer;

private bool timing;

private bool isPlayer;


    
    private void Update() {
      // if (!timer.IsOn() && beeParts.Count > 0) {destroyBeeParts();}
    }

    public void explodeEntity(Transform entity, Vector3 position, bool isPlayer) 
    {   
        parts.Clear();
        initialiseTimer();
        this.position = position;
        this.isPlayer = isPlayer;
        addParts(entity);
        addForceToParts();
        
    }

    private void initialiseTimer()
    {
        timer = new Timer();
        timer.SetOn(true);
        timer.SetCountdownSeconds(10f);
    }

    private void addParts(Transform entity) {

        foreach (Transform child in entity) {
            Debug.Log(child.gameObject.name);
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Rigidbody rigidBody = child.gameObject.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            rigidBody.mass = 0.2f;
            GameObject part = Instantiate(child.gameObject, position, Quaternion.identity);
            parts.Add(part);
        }    
         
            
    }


    private GameObject instantiatePart(GameObject part) {

      return Instantiate(part, position, Quaternion.identity);

    }

    private void addForceToParts() {
        foreach (GameObject part in parts) {
            Rigidbody rigidBody = part.GetComponent<Rigidbody>();
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

    private void destroyparts() {
        foreach (GameObject part in parts) {
            Destroy(part);
        }
        parts.Clear();
    }


}
