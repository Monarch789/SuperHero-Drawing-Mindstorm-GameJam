using System;
using UnityEngine;

public class Step2 : MonoBehaviour{

    public event EventHandler OnStepComplete;

    private void Start() {

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath(object sender, EventArgs e) {
        if (gameObject.activeSelf) {
            //if an enemy was deleted and it is the step 2 enemy
            OnStepComplete?.Invoke(this, EventArgs.Empty);
        }
    }


    private void OnDestroy() {
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
    }
}
