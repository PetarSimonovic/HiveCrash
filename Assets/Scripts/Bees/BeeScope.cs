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




    private float launchPositionY;

    // Start is called before the first frame update
    private void Awake()
    {
       lineRenderer = GetComponent<LineRenderer>();
       this.launchPositionY = 0.5f;
       bounce = false;
    }

    private bool checkCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(endPosition, transform.forward, out hit))
        {
            GameObject other = hit.collider.transform.parent.gameObject;
            if (other.tag == "rock")
            {
                Tile tile = other.GetComponent<Tile>();
                if (!tile.IsHidden())
                {
                  Debug.Log("ROCK");
                  return true;
                }
            }
        }
        return false;
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
