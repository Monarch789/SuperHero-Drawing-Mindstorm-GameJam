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

        Player.Instance.OnHealthIncreased += Player_OnHealthIncreased;
        Player.Instance.OnDamageIncreased += Player_OnDamageIncreased;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
    }

    private void Player_OnDamageIncreased(object sender, EventArgs e) {
        isDamageBuffTaken = true;
    }

    private void Player_OnHealthIncreased(object sender, EventArgs e) {
        isHealthBuffTaken = true;
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


    private void OnDestroy() {
        Player.Instance.OnHealthIncreased += Player_OnHealthIncreased;
        Player.Instance.OnDamageIncreased += Player_OnDamageIncreased;

        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
    }
}
