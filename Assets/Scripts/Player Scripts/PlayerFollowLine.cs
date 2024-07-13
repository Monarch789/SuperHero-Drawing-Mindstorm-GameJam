using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowLine : MonoBehaviour{
    //general variables
    private float speed;
    private float MinDistanceRequired;
    
    //refernce for all the positions of the line
    private Vector3[] FollowPositions;

    //bool to see if the player has donr drawing so start the follow procedure
    private bool ShouldStartFollow;

    //index to see on which index it is
    private int moveIndex;

    //event to send the player that he has followed the path so allow drawing again
    public event EventHandler OnPathFollowed;

    private void Start() {
        ShouldStartFollow = false;
        speed = 5f;
        MinDistanceRequired = 0.05f;

        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        //Get the positions for the following
        FollowPositions = new Vector3[DrawManager.Instance.GetLength()];

        DrawManager.Instance.GetPositions(FollowPositions);
        
        ShouldStartFollow = true;
        moveIndex = 0;
    }


    private void Update() {
        if (ShouldStartFollow) {

            //get the next position it has to go to
            Vector2 currentPosition = FollowPositions[moveIndex];

            //move towards the positon
            transform.position = Vector2.MoveTowards(transform.position,currentPosition,Time.deltaTime * speed);
        
            //see if the distance is less than minDistaceReqired
            if(Vector2.Distance(transform.position,currentPosition) < MinDistanceRequired) {
                //the player has almost reached the next position

                moveIndex++;

                if(moveIndex > FollowPositions.Length - 1) {
                    //the player has reached the final position on the line
                    ShouldStartFollow = false;

                    OnPathFollowed?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
