using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour{
    //Singleton
    public static Player Instance { get; private set; }

    //event to say to draw manager to start drawing
    public class OnPlayerTouchEventArgs: EventArgs { public Camera lookCamera; }

    public event EventHandler<OnPlayerTouchEventArgs> OnPlayerTouch;
    public event EventHandler OnDrawComplete;

    public event EventHandler OnPlayerPathFollowed;

    //reference of camera
    private Camera mainCam;

    //bool to see if the player started drawing
    private bool didDrawFromPlayer;

    //bool to see if the the player has followed the line so he can start drawing again
    private bool hasFollowedPath;

    //reference of the player follow line
    private PlayerFollowLine followLine;

    private void Awake() {
        Instance = this;
    
        followLine = GetComponent<PlayerFollowLine>();
    }

    private void Start() {
        mainCam = Camera.main;
        didDrawFromPlayer = false;
        hasFollowedPath = true;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
        InputManager.Instance.OnTouchEnded += InputManager_OnTouchEnded;
        followLine.OnPathFollowed += FollowLine_OnPathFollowed;

    }

    private void FollowLine_OnPathFollowed(object sender, EventArgs e) {
        hasFollowedPath = true;

        //send event to say to draw manager to destory line
        OnPlayerPathFollowed?.Invoke(this, EventArgs.Empty);
    }

    private void InputManager_OnTouchEnded(object sender, EventArgs e) {
        if (didDrawFromPlayer) {
            //the player started from he hitbox so there are lines drawn

            OnDrawComplete?.Invoke(this, EventArgs.Empty);
            didDrawFromPlayer = false;
        }
    }

    private void InputManager_OnTouchStarted(object sender, System.EventArgs e) {
        if (hasFollowedPath) {

            //see if the touch is on the player
            var rayCastHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(InputManager.Instance.GetTouchPosition()));

            if (rayCastHit /*player touch something*/ && rayCastHit.collider.gameObject.TryGetComponent(out Player player)) {
                //the player started touching from the player hitbox
                OnPlayerTouch?.Invoke(this, new OnPlayerTouchEventArgs { lookCamera = mainCam });

                //player started form hitbo
                didDrawFromPlayer = true;
                hasFollowedPath = false;

            }
        }
    }
}
