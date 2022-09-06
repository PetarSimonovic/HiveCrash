using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScope : MonoBehaviour
{

    private LineRenderer lineRenderer;

    private Vector3 startPosition;

    private Vector3 endPosition;

    private int linePoint = 1;

    private bool bounce;

    private float lineWidth = 0.1f;




    private float launchPositionY;

    // Start is called before the first frame update
    private void Awake()
    {
       calibrateLineRenderer(); 
       this.launchPositionY = 0.5f;
       bounce = false;
    }

    private void calibrateLineRenderer()
    {
       lineRenderer = GetComponent<LineRenderer>();
       lineRenderer.SetWidth(lineWidth, lineWidth);

    }

    private Vector3 calculateDirection()
    {
      Vector3 direction = -(this.endPosition - this.startPosition);
      return new Vector3(direction.x, launchPositionY, direction.z);
    }

    private void Update()
    {    
        RaycastHit hit;
        if (Physics.Raycast(lineRenderer.GetPosition(1), calculateDirection(), out hit, lineWidth))
        {
            checkIfRock(hit);
        }
    }

    private bool checkIfRock(RaycastHit hit)
    {   
        bool isRock = false;
        GameObject other = hit.collider.transform.parent.gameObject;
        if (other.tag == "rock")
        {
            Tile tile = other.GetComponent<Tile>();
            if (!tile.IsHidden())
            {
                Debug.Log("ROCK");
                isRock = true;
            }
        } 
        return isRock;
    }

    // Update is called once per frame
   public void SetLaunchPositionY(float launchPositionY)
   {
    this.launchPositionY = launchPositionY;
   }

    public void SetStartOfLine(Vector3 startPosition)
    {
        this.startPosition = startPosition;
        lineRenderer.SetPosition(0, new Vector3(startPosition.x, launchPositionY, startPosition.z));
    }

    public void SetEndOfLine(Vector3 endPosition)
    {
        this.endPosition = endPosition;
        Vector3 endOfTheLine = new Vector3(endPosition.x - startPosition.x, launchPositionY, endPosition.z - startPosition.z);
        lineRenderer.SetPosition(linePoint, new Vector3(startPosition.x - endOfTheLine.x, launchPositionY, startPosition.z - endOfTheLine.z));
    }

    public void On()
    {
      lineRenderer.enabled = true;
    }

    public void Off()
    {
       lineRenderer.enabled = false;
       bounce = false;
       this.linePoint = 1;
    }
}
