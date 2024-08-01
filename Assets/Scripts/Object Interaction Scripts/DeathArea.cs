
using System;
using UnityEngine;

public class DeathArea : MonoBehaviour{
    public static event EventHandler OnPlayerCollisionWithDeathArea;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent(out Player player)) {
            //player collided with the spikeWall
            OnPlayerCollisionWithDeathArea?.Invoke(this, EventArgs.Empty);
        }
    }
}
