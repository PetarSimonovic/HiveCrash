using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeBody : MonoBehaviour
  {
    public List<Vector3> path;

    public Vector3 hivePosition;

    private Vector3 lastTile;
    private Rigidbody rigidBody;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float angularVelocity = 0.9f;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    public bool isEnteringHive = false;

    private bool isIdling = false;

    private bool isOutsideHive = false;

    private int hiveId;



    private void Start()
    {
      isIdling = false;
      rigidBody = GetComponent<Rigidbody>();
      path = new List<Vector3>();
      hivePosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {

      rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (moveSpeed > IDLE_SPEED)
      {
        moveSpeed = moveSpeed - 0.003f;
        path.Add(transform.position);
      }
      else
      {
        moveSpeed = IDLE_SPEED;
        isIdling = true;
        float step = RETURN_SPEED * Time.deltaTime;
        Vector3 destination = path.Last();
        transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
        if (transform.position == destination)
        {
          Debug.Log("Reached destination");
          path.RemoveAt(path.Count - 1);
        }

      }
    }

    private void OnTriggerExit(Collider other)
    {
    //    isOutsideHive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
      Debug.Log(other.gameObject.name.ToString());
      if (other.gameObject.name == "hive")
      {
        Debug.Log("In Hive!");
      }


      // if (other.gameObject.tag == this.tag && isOutsideHive)
      // {
      //   isEnteringHive = true;
      // }
    }


    private void OnCollisionEnter(Collision other)
    {
      Debug.Log(other.gameObject.name);

     if (isIdling)
      {
        bounceBack(other);
      }
    }

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 999;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude);

    }

    public void SetHiveId(int hiveId)
    {
      this.hiveId = hiveId;
    }

}
