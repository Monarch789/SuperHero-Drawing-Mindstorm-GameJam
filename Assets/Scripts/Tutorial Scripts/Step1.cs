using System;
using UnityEngine;

public class Step1 : MonoBehaviour{
    public event EventHandler OnStepComplete;


    private void Start() {

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        OnStepComplete?.Invoke(this, EventArgs.Empty);
    }


    private void OnDestroy() {
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
    }
}
