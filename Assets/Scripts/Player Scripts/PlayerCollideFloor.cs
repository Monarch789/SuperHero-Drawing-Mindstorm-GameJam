using System;
using UnityEngine;

public class PlayerCollideFloor : MonoBehaviour{

    private const string FloorTag = "Floor";

    //event to send the player to cancel all movement
    public event EventHandler OnPlayerCollideWithFloorFromSide;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == FloorTag) {
            //player hit the the floor from the side

            OnPlayerCollideWithFloorFromSide?.Invoke(this, EventArgs.Empty);
        }
    }

}
