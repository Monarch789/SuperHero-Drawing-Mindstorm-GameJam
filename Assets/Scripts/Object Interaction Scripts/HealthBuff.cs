using System;
using UnityEngine;

public class HealthBuff : MonoBehaviour{
    public static event EventHandler OnHealthAdd;
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.TryGetComponent(out Player player)) {
            //player got the health upgrade

            OnHealthAdd?.Invoke(this, EventArgs.Empty);

            gameObject.SetActive(false);
        }
    }

}
