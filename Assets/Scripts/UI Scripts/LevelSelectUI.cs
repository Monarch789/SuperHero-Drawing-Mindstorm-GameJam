using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour{

    //Reference of non level buttons
    [SerializeField] private Button BackButton;
    [SerializeField] private Button TutorialButton;

    //refernce of level buttons
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button Level4Button;
    [SerializeField] private Button Level5Button;

    //string for getting total levels completed
    private const string TotalLevelsCompletedString = "LevelsCompleted";

    private int LevelsCompleted;

    //aniamtor 
    private Animator animator;


    private const string TutorialSteps = "TutorialSteps";

    private void Awake() {
        LevelsCompleted = PlayerPrefs.GetInt(TotalLevelsCompletedString,-1);

        Level1Button.interactable = LevelsCompleted == 0;
        Level2Button.interactable = LevelsCompleted == 1;
        Level3Button.interactable = LevelsCompleted == 2;
        Level4Button.interactable = LevelsCompleted == 3;
        Level5Button.interactable = LevelsCompleted == 4;

        animator = GetComponent<Animator>();

        BackButton.onClick.AddListener(() => {
            Hide();
        });

        TutorialButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt(TutorialSteps,0);
            PlayerPrefs.Save();

            Loader.LoadScene(Loader.GameScenes.TutorialScene);
        });

        //load respective scenes
        Level1Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.Level1);
        });
        Level2Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.Level2);
        });
        Level3Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.Level3);
        });
        Level4Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.Level4);
        });
        Level5Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.Level5);
        });


    }

    private void Start() {
        HideImmediately();

        MainMenu.Instance.OnPlayButtonClick += MainMenu_OnPlayButtonClick;
        GameManager.Instance.OnLevelPassed += GameManager_OnLevelPassed;

        TutorialMain.OnTutorialComplete += TutorialMain_OnTutorialComplete;
    }

    private void TutorialMain_OnTutorialComplete(object sender, System.EventArgs e) {

        if(PlayerPrefs.GetInt(TotalLevelsCompletedString,-1) < 0) {
            PlayerPrefs.SetInt(TotalLevelsCompletedString,0);
            PlayerPrefs.Save();
        }

    }

    private void GameManager_OnLevelPassed(object sender, GameManager.OnLevelCompletedEventArgs e) {
        
        if(Loader.GetCurrentScene() == Loader.GameScenes.Level1) {
            LevelsCompleted = 1;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level2) {
            LevelsCompleted = 2;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level3) {
            LevelsCompleted = 3;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level4) {
            LevelsCompleted = 4;
        }
        else if (Loader.GetCurrentScene() == Loader.GameScenes.Level5) {
            LevelsCompleted = 5;
        }

        if (LevelsCompleted > PlayerPrefs.GetInt(TotalLevelsCompletedString, -1)) {
            //if the total levels completed is greater than the previous total levels completed
            PlayerPrefs.SetInt(TotalLevelsCompletedString, LevelsCompleted);
            PlayerPrefs.Save();
        }
    }

    private void MainMenu_OnPlayButtonClick(object sender, System.EventArgs e) {
        Show();
    }

    private void HideImmediately() {
        gameObject.SetActive(false);
    }
    private void Show() {
        gameObject.SetActive(true);

        animator.SetTrigger("OnPlayClick");
    }

    private void Hide() {
        animator.SetTrigger("OnLevelSelectBack");

        StartCoroutine(AnimationHideDelay());
    }

    private void OnDestroy() {
        MainMenu.Instance.OnPlayButtonClick -= MainMenu_OnPlayButtonClick;
        GameManager.Instance.OnLevelPassed -= GameManager_OnLevelPassed;
    }

    private IEnumerator AnimationHideDelay() {
        yield return new WaitForSeconds(0.5f);

        HideImmediately();
    }


}
