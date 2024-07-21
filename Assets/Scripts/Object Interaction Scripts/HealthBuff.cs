using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour{
    public class OnHealthAddEventArgs : EventArgs { public float addHealth; }
    public static event EventHandler<OnHealthAddEventArgs> OnHealthAdd;

    //float to see how mmuch health is added
    private float addedHealth = 5f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.TryGetComponent(out Player player)) {
            //player got the health upgrade

            OnHealthAdd?.Invoke(this, new OnHealthAddEventArgs { addHealth = addedHealth });

            gameObject.SetActive(false);
        }
    }

}
