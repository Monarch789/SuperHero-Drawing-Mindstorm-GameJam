using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour{
    //Singleton
    public static DrawManager Instance {  get; private set; }

    //Camera Reference
    private Camera mainCam;

    //refernce for spawning the line
    [SerializeField] private Line linePrefab;
    private Line currentLine;

    //resolution for reducing jankiness
    public const float LineResolution = 0.1f;
    public float MaxPoints;

    //bool to see if we should draw i.e when the player starts pressing on the screen
    private bool ShouldStartDrawing;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ShouldStartDrawing = false;

        Player.Instance.OnPlayerTouch += Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;

        MaxPoints = 100;
    }

    private void Player_OnPlayerPathFollowed(object sender, System.EventArgs e) {
        //destroy the line
        if(currentLine == null) {
            Debug.LogError("There is a line drawn when its not supposed to!");
        }

        Destroy(currentLine.gameObject);
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

    public void GetPositions(Vector3[] FollowPositions) {
        currentLine.GetPositions(FollowPositions);
    }

    public int GetLength() {
        return currentLine.GetPointsCount();
    }
}
