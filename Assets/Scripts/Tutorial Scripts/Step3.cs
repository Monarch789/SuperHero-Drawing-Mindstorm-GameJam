using System;
using System.Collections;
using System.Collections.Generic;
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

        TutorialMain.Instance.OnStepObjectActivate += TutorialMain_OnStepObjectActivate;

        HealthBuff.OnHealthAdd += HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd += DamageBuff_OnDamageAdd;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath(object sender, EventArgs e) {
        if(gameObject.activeSelf)
            EnemiesKilled++;

        if(EnemiesKilled >= 2) {
            //check if both buffs were taken
            if(isHealthBuffTaken && isDamageBuffTaken) {
                OnStepComplete?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void DamageBuff_OnDamageAdd(object sender, DamageBuff.OnDamageAddEventArgs e) {
        isDamageBuffTaken = true;
    }

    private void HealthBuff_OnHealthAdd(object sender, HealthBuff.OnHealthAddEventArgs e) {
        isHealthBuffTaken = true;
    }

    private void TutorialMain_OnStepObjectActivate(object sender, TutorialMain.OnStepObjectActivateEventArgs e) {
        //this will be activated if 2 steps are done

        if (e.stepsNumber == 2) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        TutorialMain.Instance.OnStepObjectActivate -= TutorialMain_OnStepObjectActivate;

        HealthBuff.OnHealthAdd -= HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd -= DamageBuff_OnDamageAdd;

        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
    }
}
