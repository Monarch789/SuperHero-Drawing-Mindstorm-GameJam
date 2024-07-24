using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaitUI : MonoBehaviour{

    private void Start() {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;    
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }
}
