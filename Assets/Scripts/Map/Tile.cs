using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Material meadow;

    private GameObject hex;

    private MeshRenderer renderer;
    // Start is called before the first frame update
    public void Start()
    {
      hex = this.gameObject.transform.GetChild(0).gameObject;
      Debug.Log(hex);

      renderer = hex.GetComponent<MeshRenderer>();
      Debug.Log("Renderer");
      Debug.Log(renderer.material);




    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void DisperseFog()
    {

    }

    // public void OnCollisionEnter(Collision collision)
    // {
    //   Debug.Log(transform.position);
    //   Debug.Log(renderer.material.color);
    //   renderer.material.color = Color.green;
    //   Debug.Log(renderer.material.color);
    //
    //
    // }
    //
    public void OnTriggerEnter(Collider collision)
    {
      Debug.Log(transform.position);
      Debug.Log(renderer.material.color);
      renderer.material.color = Color.green;
      Debug.Log(renderer.material.color);
    }


}
