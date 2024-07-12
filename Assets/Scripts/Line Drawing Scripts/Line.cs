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

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1,position);
    }

    private bool CanAppend(Vector2 position) {
        if (lineRenderer.positionCount == 0) return true;

        //if the distance between current drawing position and last drawn position is greater than resolution then return true so that it draw another line
        return Vector2.Distance(position, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > DrawManager.LineResolution;
    }

    public int GetPointsCount() {
        return lineRenderer.positionCount;
    }

}
