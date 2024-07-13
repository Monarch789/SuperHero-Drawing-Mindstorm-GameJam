using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Line : MonoBehaviour{

    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPosition(Vector2 position) {
        if (!CanAppend(position)) return;

        //extrude the vertex
        lineRenderer.positionCount++;
        
        //set the position of extruded vertex to the mouse position
        lineRenderer.SetPosition(lineRenderer.positionCount - 1,position);
    }

    private bool CanAppend(Vector2 position) {
        // if there is no vertex yet then return true

        if (lineRenderer.positionCount == 0)
            return true;

        //if the distance between current drawing position and last drawn position is greater than resolution then return true so that it draw another line
        return Vector2.Distance(position, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > DrawManager.LineResolution;
    }

    public int GetPointsCount() {
        return lineRenderer.positionCount;
    }

}
