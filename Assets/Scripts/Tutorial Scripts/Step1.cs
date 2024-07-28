using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Step1 : MonoBehaviour{
    public event EventHandler OnStepComplete;


    private void Start() {
        TutorialMain.Instance.OnStepObjectActivate += TutorialMain_OnStepObjectActivate;

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        OnStepComplete?.Invoke(this, EventArgs.Empty);
    }

    private void TutorialMain_OnStepObjectActivate(object sender, TutorialMain.OnStepObjectActivateEventArgs e) {
        //this will be activated if no steps are done

        if(e.stepsNumber == 0) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        TutorialMain.Instance.OnStepObjectActivate -= TutorialMain_OnStepObjectActivate;
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
    }
}
