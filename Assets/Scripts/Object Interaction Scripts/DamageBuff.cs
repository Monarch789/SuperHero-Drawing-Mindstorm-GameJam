using System;
using UnityEngine;

public class DamageBuff : MonoBehaviour{

    public static event EventHandler OnDamageAdd;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.TryGetComponent(out Player player)) {
            //player got the damage upgrade

            OnDamageAdd?.Invoke(this, EventArgs.Empty);

            //delete this object
            gameObject.SetActive(false);
        }
    }

}
