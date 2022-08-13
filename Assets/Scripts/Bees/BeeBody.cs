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
    private float moveSpeed = 15f;

    [SerializeField]
    private float angularVelocity = 0.9f;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    public bool isEnteringHive = false;

    private bool isIdling = false;

    private bool isOutsideHive = false;



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
      transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

      // if (moveSpeed > IDLE_SPEED)
      // {
      //   moveSpeed = moveSpeed - 0.003f;
      //   path.Add(transform.position);
      // }
      // else
      // {
      //   moveSpeed = IDLE_SPEED;
      //   isIdling = true;
      //   float step = RETURN_SPEED * Time.deltaTime;
      //   Vector3 destination = path.Last();
      //   transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
      //   if (transform.position == destination)
      //   {
      //     Debug.Log("Reached destination");
      //     path.RemoveAt(path.Count - 1);
      //     Debug.Log(path.Count);
      //   }
      //
      // }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     isOutsideHive = true;
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //   if (other.gameObject.tag == this.tag && isOutsideHive)
    //   {
    //     isEnteringHive = true;
    //   }
    // }
    //
    //
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //
    //  if (isIdling)
    //   {
    //     bounceBack(other);
    //   }
    // }
    //
    // private void bounceBack(Collision2D other)
    // {
    //
    //     // how much the character should be knocked back
    //     var magnitude = 999;
    //     // calculate force vector
    //     var force = transform.position - other.transform.position;
    //     // normalize force vector to get direction only and trim magnitude
    //     force.Normalize();
    //     rigidBody2d.AddForce(force * magnitude);
    //
    // }

}
