using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour{
    public static event EventHandler OnPlayerCollisionWithSpikeWall;

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.TryGetComponent(out Player player)) {
            //player collided with the spikeWall
            OnPlayerCollisionWithSpikeWall?.Invoke(this, EventArgs.Empty);
        }
    }

}
