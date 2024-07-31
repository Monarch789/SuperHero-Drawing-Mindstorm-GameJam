using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour{

    //strings for triggers
    private const string LevelFailed_Trigger = "LevelFailed";
    private const string LevelPassed_0_Trigger = "LevelPassed0";
    private const string LevelPassed_1_Trigger = "LevelPassed1";
    private const string LevelPassed_2_Trigger = "LevelPassed2";
    private const string LevelPassed_3_Trigger = "LevelPassed3";

    [SerializeField] private TextMeshProUGUI LevelCompletedText;

    [SerializeField] private Button NextLevelButton;
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button MenuButton;

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();

        RetryButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadCurrentScene();
        });
        MenuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.MainMenu);
        });

        NextLevelButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadNextLevel();
        });
    }

    private void Start() {
        Hide();

        GameManager.Instance.OnLevelFailed += GameManager_OnLevelFailed;
        GameManager.Instance.OnLevelPassed += GameManager_OnLevelPassed;
        
    }

    private void GameManager_OnLevelPassed(object sender, GameManager.OnLevelCompletedEventArgs e) {
        LevelCompletedText.text = "Level Passed";

        //see how many stars and set teigger accordingly
        Show();
        
        switch (e.Stars) {
            case 0:
                animator.SetTrigger(LevelPassed_0_Trigger);
                break;
            case 1:
                animator.SetTrigger(LevelPassed_1_Trigger);
                break;
            case 2:
                animator.SetTrigger(LevelPassed_2_Trigger);
                break;
            case 3:
                animator.SetTrigger(LevelPassed_3_Trigger);
                break;
            default:
                animator.SetTrigger(LevelFailed_Trigger);
                break;
        }
    }

    private void GameManager_OnLevelFailed(object sender, System.EventArgs e) {
        NextLevelButton.interactable = false;

        LevelCompletedText.text = "Level Failed";
        
        Show();
        
        animator.SetTrigger(LevelFailed_Trigger);
    }

    private void OnDestroy() {
        GameManager.Instance.OnLevelFailed -= GameManager_OnLevelFailed;
        GameManager.Instance.OnLevelPassed -= GameManager_OnLevelPassed;
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }

}
