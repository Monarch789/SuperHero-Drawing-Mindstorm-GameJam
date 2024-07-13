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

    //reference of camera
    private Camera mainCam;

    //bool to see if the player started drawing
    private bool didDrawFromPlayer;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        mainCam = Camera.main;
        didDrawFromPlayer = false;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
        InputManager.Instance.OnTouchEnded += InputManager_OnTouchEnded;
    }

    private void InputManager_OnTouchEnded(object sender, EventArgs e) {
        if (didDrawFromPlayer) {
            //the player started from he hitbox so there are lines drawn

            OnDrawComplete?.Invoke(this, EventArgs.Empty);
            didDrawFromPlayer = false;
        }
    }

    private void InputManager_OnTouchStarted(object sender, System.EventArgs e) {
        //see if the touch is on the player
        var rayCastHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(InputManager.Instance.GetTouchPosition()));

        if(rayCastHit /*player touch something*/ && rayCastHit.collider.gameObject.TryGetComponent(out Player player)) {
            //the player started touching from the player hitbox
            OnPlayerTouch?.Invoke(this, new OnPlayerTouchEventArgs { lookCamera = mainCam });

            //player started form hitbo
            didDrawFromPlayer = true;
        }
    }
}
