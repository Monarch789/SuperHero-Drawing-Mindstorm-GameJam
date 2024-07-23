using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    //Singleton
    public static GameManager Instance {  get; private set; }

    //ints to see how many enemies required for completion, one star, 2 star and 3 star
    [SerializeField] private int CompletionEnemies;
    [SerializeField] private int OneStarEnemies;
    [SerializeField] private int TwoStarEnemies;
    [SerializeField] private int ThreeStarEnemies;

    //events of Pause and UnPause to send other scripts to not do stuff
    public event EventHandler OnPause;
    public event EventHandler OnUnPause;

    //int to see ow many enemies have been killed
    private int EnemiesKilled;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        EnemiesKilled = 0;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;

        Player.Instance.OnDeath += Player_OnDeath;
        PauseMenu.Instance.OnPauseButtonClick += PauseMenu_OnPauseButtonClick;
        PauseMenu.Instance.OnPlayButtonClick += PauseMenu_OnPlayButtonClick;
    }

    private void PauseMenu_OnPlayButtonClick(object sender, System.EventArgs e) {
        Time.timeScale = 1f;

        OnUnPause?.Invoke(this, EventArgs.Empty);
    }

    private void PauseMenu_OnPauseButtonClick(object sender, System.EventArgs e) {
        Time.timeScale = 0f;

        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void Player_OnDeath(object sender, System.EventArgs e) {
        //check how the level is completed

        if(EnemiesKilled < CompletionEnemies) {
            Debug.Log("Level failed");
        }
        else if(EnemiesKilled < OneStarEnemies) {
            Debug.Log("level passed with 0 stars");
        }
        else if(EnemiesKilled < TwoStarEnemies) {
            Debug.Log("level passed with 1 stars");
        } 
        else if(EnemiesKilled < ThreeStarEnemies) {
            Debug.Log("level passed with 2 stars");
        }
        else {
            Debug.Log("level passed with 3 stars");
        }
        
    }

    private void Enemy_OnEnemyDeath(object sender, System.EventArgs e) {
        EnemiesKilled++;
    }

    private void OnDestroy() {
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
        Player.Instance.OnDeath -= Player_OnDeath;
        PauseMenu.Instance.OnPauseButtonClick -= PauseMenu_OnPauseButtonClick;
        PauseMenu.Instance.OnPlayButtonClick -= PauseMenu_OnPlayButtonClick;
    }
}
