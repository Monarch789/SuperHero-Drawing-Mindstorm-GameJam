using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2 : MonoBehaviour{

    public event EventHandler OnStepComplete;

    private void Start() {
        TutorialMain.Instance.OnStepObjectActivate += TutorialMain_OnStepObjectActivate;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath(object sender, EventArgs e) {
        if (gameObject.activeSelf) {
            //if an enemy was deleted and it is the step 2 enemy
            OnStepComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    private void TutorialMain_OnStepObjectActivate(object sender, TutorialMain.OnStepObjectActivateEventArgs e) {
        //this will be activated if 1 step is done

        if (e.stepsNumber == 1) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        TutorialMain.Instance.OnStepObjectActivate -= TutorialMain_OnStepObjectActivate;
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
    }
}
