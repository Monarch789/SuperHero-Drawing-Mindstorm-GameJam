using System;
using UnityEngine;

public class PlayerCollideFloor : MonoBehaviour{

    private const string SpikeTag = "Spike";

    //event to send the player to cancel all movement
    public event EventHandler OnPlayerCollideWithFloorFromSide;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == SpikeTag) {
            //player hit the the floor from the side

            OnPlayerCollideWithFloorFromSide?.Invoke(this, EventArgs.Empty);
        }
    }

}
