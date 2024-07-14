using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowLine : MonoBehaviour{
    //general variables
    private float speed;
    private float MinDistanceRequired;
    private float ForceMultiplier;

    //refernce for all the positions of the line
    private Vector3[] FollowPositions;

    //bool to see if the player has donr drawing so start the follow procedure
    private bool ShouldStartFollow;

    //index to see on which index it is
    private int moveIndex;

    //event to send the player that he has followed the path so allow drawing again
    public event EventHandler OnPathFollowed;

    //reference of rigdbody
    private Rigidbody2D rigidbodyComponent;

    //direction of force to be applied after its done
    private Vector2 Direction;

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ShouldStartFollow = false;
        speed = 5f;
        MinDistanceRequired = 0.05f;
        ForceMultiplier = 65f;

        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
        Player.Instance.OnPlayerMoveStop += Player_OnPlayerMoveStop;
    
    }

    private void Player_OnPlayerMoveStop(object sender, EventArgs e) {
        ShouldStartFollow = false;

        rigidbodyComponent.gravityScale = 1f;

        //get directio of force
        Direction = ( -FollowPositions[moveIndex] + FollowPositions[moveIndex-1]).normalized;
        rigidbodyComponent.AddForce(Direction * speed * ForceMultiplier, ForceMode2D.Force);

        OnPathFollowed?.Invoke(this, EventArgs.Empty);
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        //Get the positions for the following
        FollowPositions = new Vector3[DrawManager.Instance.GetLength()];

        DrawManager.Instance.GetPositions(FollowPositions);
        
        ShouldStartFollow = true;
        moveIndex = 0;

        //get the direction of the force to be applied
        Direction = (FollowPositions[FollowPositions.Length - 1] - FollowPositions[FollowPositions.Length - 2]).normalized;
    }


    private void Update() {
        if (ShouldStartFollow) {

            //turn off gravity
            rigidbodyComponent.gravityScale = 0f;

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

                    //Turn on gravity
                    rigidbodyComponent.gravityScale = 1f;
                    rigidbodyComponent.AddForce(Direction * speed * ForceMultiplier, ForceMode2D.Force);
                }
            }
        }
    }
}
