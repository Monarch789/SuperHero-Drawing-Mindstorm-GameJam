using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour{
    //Singleton
    public static GameManager Instance {  get; private set; }

    //ints to see how many enemies required for completion, one star, 2 star and 3 star
    [SerializeField] private int CompletionEnemies;
    [SerializeField] private int OneStarEnemies;
    [SerializeField] private int TwoStarEnemies;
    [SerializeField] private int ThreeStarEnemies;

    [SerializeField] private Camera mainCam;

    private const string TotalLevelsCompletedString = "LevelsCompleted";
    private int LevelsCompleted;

    //stars of all levels
    private readonly string[] LevelsStarsStrings = { "Level1Stars", "Level2Stars", "Level3Stars", "Level4Stars", "Level5Stars", "Level6Stars", "Level7Stars", "Level8Stars", "Level9Stars", "Level10Stars", "Level11Stars", "Level12Stars" };

    //events of Pause and UnPause to send other scripts to not do stuff
    public event EventHandler OnPause;
    public event EventHandler OnUnPause;

    //int to see ow many enemies have been killed
    private int EnemiesKilled;

    //bool to see if the level is waiting to start
    private bool isGameWaitingToStart;

    //event to send to tell that game is started
    public event EventHandler OnGameStarted;

    //events to send On level failed and complete
    public event EventHandler OnLevelFailed;

    public class OnLevelCompletedEventArgs : EventArgs { public int Stars; }
    public event EventHandler<OnLevelCompletedEventArgs> OnLevelPassed;

    
    
    private void Awake() {
        Instance = this;

        LevelsCompleted = PlayerPrefs.GetInt(TotalLevelsCompletedString,-1);
    }

    private void Start() {
        isGameWaitingToStart = false;

        EnemiesKilled = 0;

        Time.timeScale = 1f;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;

        PauseMenu.Instance.OnPauseButtonClick += PauseMenu_OnPauseButtonClick;
        PauseMenu.Instance.OnPlayButtonClick += PauseMenu_OnPlayButtonClick;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        //check how the level is completed

        int StarsGot = 0;

        if (EnemiesKilled < CompletionEnemies) {
            OnLevelFailed?.Invoke(this, EventArgs.Empty);
        }
        else if (EnemiesKilled < OneStarEnemies) {
            StarsGot = 0;

            OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 0 });
        }
        else if (EnemiesKilled < TwoStarEnemies) {
            StarsGot = 1;

            OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 1 });
        }
        else if (EnemiesKilled < ThreeStarEnemies) {
            StarsGot = 2;

            OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 2 });
        }
        else {
            StarsGot = 3;

            OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 3});
        }

        if (Loader.GetCurrentScene() == Loader.GameScenes.Level1) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[0],0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[0],StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 1;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level2) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[1], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[1], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 2;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level3) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[2], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[2], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 3;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level4) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[3], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[3], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 4;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level5) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[4], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[4], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 5;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level6) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[5], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[5], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 6;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level7) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[6], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[6], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 7;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level8) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[7], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[7], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 8;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level9) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[8], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[8], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 9;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level10) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[9], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[9], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 10;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level11) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[10], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[10], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 11;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level12) {
            if (PlayerPrefs.GetInt(LevelsStarsStrings[11], 0) < StarsGot) {
                PlayerPrefs.SetInt(LevelsStarsStrings[11], StarsGot);
                PlayerPrefs.Save();
            }

            LevelsCompleted = 12;
        }

        if (LevelsCompleted > PlayerPrefs.GetInt(TotalLevelsCompletedString, -1)) {
            //if the total levels completed is greater than the previous total levels completed
            PlayerPrefs.SetInt(TotalLevelsCompletedString, LevelsCompleted);
            PlayerPrefs.Save();
        }
    }

    private void InputManager_OnTouchStarted(object sender, EventArgs e) {
        if (!isGameWaitingToStart){

            var rayCastHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(InputManager.Instance.GetTouchPosition()));
            
            if (rayCastHit && rayCastHit.transform.tag == "LevelStartHitbox") {
                OnGameStarted?.Invoke(this, EventArgs.Empty);
                isGameWaitingToStart = true;

            }
        }
    }

    private void PauseMenu_OnPlayButtonClick(object sender, System.EventArgs e) {
        Time.timeScale = 1f;

        OnUnPause?.Invoke(this, EventArgs.Empty);
    }

    private void PauseMenu_OnPauseButtonClick(object sender, System.EventArgs e) {
        Time.timeScale = 0f;

        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void Enemy_OnEnemyDeath(object sender, System.EventArgs e) {
        EnemiesKilled++;
    }

    private void OnDestroy() {
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
        PauseMenu.Instance.OnPauseButtonClick -= PauseMenu_OnPauseButtonClick;
        PauseMenu.Instance.OnPlayButtonClick -= PauseMenu_OnPlayButtonClick;
        InputManager.Instance.OnTouchStarted -= InputManager_OnTouchStarted;
    }

}
