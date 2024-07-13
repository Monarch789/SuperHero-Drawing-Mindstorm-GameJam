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
    public const float LineResolution = 0.1f;

    //bool to see if we should draw i.e when the player starts pressing on the screen
    private bool ShouldStartDrawing;


    private void Awake() {
        mainCam = Camera.main;
    }

    private void Start() {
        ShouldStartDrawing = false;

        Player.Instance.OnPlayerTouch += Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        ShouldStartDrawing = false;
    }

    private void Player_OnPlayerTouch(object sender, Player.OnPlayerTouchEventArgs e) {
        mainCam = e.lookCamera;

        ShouldStartDrawing = true;

        //Get the world pos
        Vector2 MouseWorldPos = mainCam.ScreenToWorldPoint(InputManager.Instance.GetTouchPosition());

        //make a new Line i.e object with line renderer
        currentLine = Instantiate(linePrefab, MouseWorldPos, Quaternion.identity);
    }

    private void Update() {
        if (ShouldStartDrawing) {

            Vector2 MouseWorldPos = mainCam.ScreenToWorldPoint(InputManager.Instance.GetTouchPosition());
            
            //say the line to extend or extrude its vertices
            currentLine.SetPosition(MouseWorldPos);
        }
    }
}
