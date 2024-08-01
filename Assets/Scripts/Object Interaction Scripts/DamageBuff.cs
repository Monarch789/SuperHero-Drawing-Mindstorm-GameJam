using System;
using UnityEngine;

public class DamageBuff : MonoBehaviour{

    public class OnDamageAddEventArgs:EventArgs { public float addDamage; }
    public static event EventHandler<OnDamageAddEventArgs> OnDamageAdd;

    public float addedDamage = 3f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.TryGetComponent(out Player player)) {
            //player got the damage upgrade

            OnDamageAdd?.Invoke(this, new OnDamageAddEventArgs { addDamage = addedDamage });

            //delete this object
            gameObject.SetActive(false);
        }
    }

}
