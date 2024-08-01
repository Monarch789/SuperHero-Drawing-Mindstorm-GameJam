using System;
using UnityEngine;

public class PlayerCollideFloorDownwards : MonoBehaviour{

    private const string FloorTag = "Floor";

    public event EventHandler OnColliderFloorFromDown;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == FloorTag) {
            OnColliderFloorFromDown?.Invoke(this, EventArgs.Empty);
        }
    }

}
