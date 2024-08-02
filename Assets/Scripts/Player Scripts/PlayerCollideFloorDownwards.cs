using System;
using UnityEngine;

public class PlayerCollideFloorDownwards : MonoBehaviour{

    private const string SpikeTag = "Spike";

    public event EventHandler OnColliderFloorFromDown;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == SpikeTag) {
            OnColliderFloorFromDown?.Invoke(this, EventArgs.Empty);
        }
    }

}
