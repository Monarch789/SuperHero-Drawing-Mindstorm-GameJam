using System;
using UnityEngine;

public class Step3 : MonoBehaviour{

    public event EventHandler OnStepComplete;

    //bool to see if both buffs have been taken
    private bool isHealthBuffTaken;
    private bool isDamageBuffTaken;

    private int EnemiesKilled;

    private void Start() {
        isHealthBuffTaken = false;
        isDamageBuffTaken = false;
        EnemiesKilled = 0;

        HealthBuff.OnHealthAdd += HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd += DamageBuff_OnDamageAdd;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        if (EnemiesKilled >= 2) {
            //check if both buffs were taken
            if (isHealthBuffTaken && isDamageBuffTaken) {
                OnStepComplete?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void Enemy_OnEnemyDeath(object sender, EventArgs e) {
        if(gameObject.activeSelf)
            EnemiesKilled++;
    }

    private void DamageBuff_OnDamageAdd(object sender, DamageBuff.OnDamageAddEventArgs e) {
        isDamageBuffTaken = true;
    }

    private void HealthBuff_OnHealthAdd(object sender, HealthBuff.OnHealthAddEventArgs e) {
        isHealthBuffTaken = true;
    }

    private void OnDestroy() {

        HealthBuff.OnHealthAdd -= HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd -= DamageBuff_OnDamageAdd;

        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
    }
}
