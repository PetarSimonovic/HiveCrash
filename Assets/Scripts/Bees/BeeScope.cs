using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScope : MonoBehaviour
{

    private LineRenderer lineRenderer;

    private Vector3 startPosition;

    private Vector3 endPosition;

    private Vector3 hitNormal;

    private bool bounce;

    private float lineWidth = 0.05f;

    private int linePosition = 1;




    private float launchPositionY;

    // Start is called before the first frame update
    private void Awake()
    {
       calibrateLineRenderer(); 
       resetLinePositions();
       this.launchPositionY = 0.5f;
    }

    private void calibrateLineRenderer()
    {
       lineRenderer = GetComponent<LineRenderer>();
       lineRenderer.SetWidth(lineWidth, lineWidth);
       lineRenderer.positionCount = 3;
    }

    private Vector3 calculateDirection()
    {
      Vector3 direction = -(this.endPosition - this.startPosition);
      return new Vector3(direction.x, launchPositionY, direction.z);
    }

    private bool checkForRockCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(lineRenderer.GetPosition(1), calculateDirection(), out hit, lineWidth * 3))
        {
         if (checkIfValidCollision(hit))
         {
            SetEndPosition(hit.point);
            hitNormal = hit.normal;
            return true;
         }
        }
         return false;
    }

    private bool checkIfValidCollision(RaycastHit hit)
    {   
        GameObject other = hit.collider.transform.parent.gameObject;
        if (other.tag == "rock")
        {
            return checkIfRockVisible(other);
        } 
        return false;
    }

    private bool checkIfRockVisible(GameObject other)
    {
        Tile tile = other.GetComponent<Tile>();
        return !tile.IsHidden();
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

    public void SetEndPosition(Vector3 endPosition)
    {
            this.endPosition = endPosition;
    }

    public void DrawLine(Vector3 endDragPosition)
    {
        SetEndPosition(endDragPosition);
        drawLine(1);
        drawLine(2);
        if (checkForRockCollision())
        {
            processReflectedRay();
            lineRenderer.SetPosition(1, endPosition);
        }
        
    }

    private void processReflectedRay()
    {
        Vector3 incomingVec = endPosition - startPosition;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hitNormal);
        lineRenderer.SetPosition(2, reflectVec);
    }

    private void drawLine(int lineNode)
    {
        Vector3 endOfTheLine = new Vector3(endPosition.x - startPosition.x, launchPositionY, endPosition.z - startPosition.z);
        lineRenderer.SetPosition(lineNode, new Vector3(startPosition.x - endOfTheLine.x, launchPositionY, startPosition.z - endOfTheLine.z));
    }

 

    private void drawReflectedLine(RaycastHit hit)
    {
        Vector3 incomingVec = hit.point - startPosition;
        Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
        lineRenderer.SetPosition(2, reflectVec);
    }

    public void On()
    {
      lineRenderer.enabled = true;
    }

    public void Off()
    {
       resetLinePositions();
       lineRenderer.enabled = false;
       bounce = false;
    }

    private void resetLinePositions()
    {
        var points = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
         points[i] = startPosition;
        }
        lineRenderer.SetPositions(points);
    }
}
