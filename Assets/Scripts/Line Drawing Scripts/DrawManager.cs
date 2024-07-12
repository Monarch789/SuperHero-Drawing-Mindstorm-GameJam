using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour{

    //Camera Reference
    private Camera mainCam;

    //refernce for spawning the line
    [SerializeField] private Line linePrefab;
    private Line currentLine;

    //resolution for reducing jankiness
    public const float LineResolution = 0.2f;

    //bool to see if we should draw i.e when the player starts pressing on the screen
    private bool ShouldStartDrawing;


    private void Awake() {
        mainCam = Camera.main;
    }

    private void Start() {
        ShouldStartDrawing = false;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
        InputManager.Instance.OnTouchEnded += InputManager_OnTouchEnded;
    }

    private void InputManager_OnTouchEnded(object sender, System.EventArgs e) {
        ShouldStartDrawing = false;

        if(currentLine.GetPointsCount() < 2) {
            //there exists no line with only 1 or 0 vertices so destroy that line
            Destroy(currentLine.gameObject);
        }
    }

    private void InputManager_OnTouchStarted(object sender, System.EventArgs e) {
        ShouldStartDrawing = true;

        //Get the world pos
        Vector2 MouseWorldPos = mainCam.ScreenToWorldPoint(InputManager.Instance.GetTouchPosition());
        currentLine = Instantiate(linePrefab,MouseWorldPos,Quaternion.identity);
    }

    private void Update() {
        if (ShouldStartDrawing) {

            Vector2 MouseWorldPos = mainCam.ScreenToWorldPoint(InputManager.Instance.GetTouchPosition());
            currentLine.SetPosition(MouseWorldPos);
        }
    }
}
