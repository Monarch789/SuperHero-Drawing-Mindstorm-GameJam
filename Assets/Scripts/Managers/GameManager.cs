using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance { get; private set; }

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

    [SerializeField] private bool isOnTutorial = false;

    private bool isLevelDone;

    public class OnLevelCompletedEventArgs : EventArgs { public int Stars; }
    public event EventHandler<OnLevelCompletedEventArgs> OnLevelPassed;

    public event EventHandler OnLevelDone;



    private void Awake()
    {
        Instance = this;

        LevelsCompleted = PlayerPrefs.GetInt(TotalLevelsCompletedString, -1);
    }

    private void Start()
    {
        isGameWaitingToStart = false;
        isLevelDone = false;

        EnemiesKilled = 0;

        Time.timeScale = 1f;

        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;

        //PauseMenu.Instance.OnPauseButtonClick += PauseMenu_OnPauseButtonClick;
        //PauseMenu.Instance.OnPlayButtonClick += PauseMenu_OnPlayButtonClick;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e)
    {
        //check how the level is completed
        if (!isOnTutorial)
        {

            isLevelDone = true;
            OnLevelDone?.Invoke(this, EventArgs.Empty);

            int StarsGot = 0;

            if (EnemiesKilled < CompletionEnemies)
            {
                OnLevelFailed?.Invoke(this, EventArgs.Empty);
            }
            else if (EnemiesKilled < OneStarEnemies)
            {
                StarsGot = 0;

                OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 0 });
            }
            else if (EnemiesKilled < TwoStarEnemies)
            {
                StarsGot = 1;

                OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 1 });
            }
            else if (EnemiesKilled < ThreeStarEnemies)
            {
                StarsGot = 2;

                OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 2 });
            }
            else
            {
                StarsGot = 3;

                OnLevelPassed?.Invoke(this, new OnLevelCompletedEventArgs { Stars = 3 });
            }

            string CurrentLevel = Loader.GetCurrentScene().ToString();

            if (CurrentLevel != Loader.GameScenes.TutorialScene.ToString())
            {
                int levelNumber = int.Parse(CurrentLevel.Substring(5));


                if (PlayerPrefs.GetInt(LevelsStarsStrings[levelNumber - 1], 0) < StarsGot)
                {
                    PlayerPrefs.SetInt(LevelsStarsStrings[levelNumber - 1], StarsGot);
                    PlayerPrefs.Save();
                }

                LevelsCompleted = levelNumber;
            }

            if (LevelsCompleted > PlayerPrefs.GetInt(TotalLevelsCompletedString, -1))
            {
                //if the total levels completed is greater than the previous total levels completed
                PlayerPrefs.SetInt(TotalLevelsCompletedString, LevelsCompleted);
                PlayerPrefs.Save();
            }
        }
    }

    private void InputManager_OnTouchStarted(object sender, EventArgs e)
    {
        if (!isGameWaitingToStart)
        {

            var rayCastHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(InputManager.Instance.GetTouchPosition()));

            if (rayCastHit && rayCastHit.transform.tag == "LevelStartHitbox")
            {
                OnGameStarted?.Invoke(this, EventArgs.Empty);
                isGameWaitingToStart = true;

            }
        }
    }

    private void PauseMenu_OnPlayButtonClick(object sender, System.EventArgs e)
    {
        Time.timeScale = 1f;

        OnUnPause?.Invoke(this, EventArgs.Empty);
    }

    private void PauseMenu_OnPauseButtonClick(object sender, System.EventArgs e)
    {
        Time.timeScale = 0f;

        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void Enemy_OnEnemyDeath(object sender, System.EventArgs e)
    {
        if (!isLevelDone)
            EnemiesKilled++;
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
        Enemy.OnEnemyDeath -= Enemy_OnEnemyDeath;
        PauseMenu.Instance.OnPauseButtonClick -= PauseMenu_OnPauseButtonClick;
        PauseMenu.Instance.OnPlayButtonClick -= PauseMenu_OnPlayButtonClick;
        InputManager.Instance.OnTouchStarted -= InputManager_OnTouchStarted;
    }

}
