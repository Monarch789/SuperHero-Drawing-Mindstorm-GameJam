using System.Collections;
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
    [SerializeField] private Button Level6Button;
    [SerializeField] private Button Level7Button;
    [SerializeField] private Button Level8Button;
    [SerializeField] private Button Level9Button;
    [SerializeField] private Button Level10Button;
    [SerializeField] private Button Level11Button;
    [SerializeField] private Button Level12Button;

    //string for getting total levels completed
    private const string TotalLevelsCompletedString = "LevelsCompleted";

    private int LevelsCompleted;

    //aniamtor 
    private Animator animator;


    private const string TutorialSteps = "TutorialSteps";

    private void Awake() {
        LevelsCompleted = PlayerPrefs.GetInt(TotalLevelsCompletedString,-1);

        Level1Button.interactable = LevelsCompleted >= 0;
        Level2Button.interactable = LevelsCompleted >= 1;
        Level3Button.interactable = LevelsCompleted >= 2;
        Level4Button.interactable = LevelsCompleted >= 3;
        Level5Button.interactable = LevelsCompleted >= 4;
        Level6Button.interactable = LevelsCompleted >= 5;
        Level7Button.interactable = LevelsCompleted >= 6;
        Level8Button.interactable = LevelsCompleted >= 7;
        Level9Button.interactable = LevelsCompleted >= 8;
        Level10Button.interactable = LevelsCompleted >= 9;
        Level11Button.interactable = LevelsCompleted >= 10;
        Level12Button.interactable = LevelsCompleted >= 11;

        animator = GetComponent<Animator>();

        BackButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Hide();
        });

        TutorialButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            PlayerPrefs.SetInt(TutorialSteps,0);
            PlayerPrefs.Save();

            Loader.LoadScene(Loader.GameScenes.TutorialScene);
        });

        //load respective scenes
        Level1Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level1);
        });
        Level2Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level2);
        });
        Level3Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level3);
        });
        Level4Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level4);
        });
        Level5Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level5);
        });
        Level6Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level6);
        });
        Level7Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level7);
        });
        Level8Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level8);
        });
        Level9Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level9);
        });
        Level10Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level10);
        });
        Level11Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level11);
        });
        Level12Button.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.Level12);
        });

    }

    private void Start() {
        HideImmediately();

        MainMenu.Instance.OnPlayButtonClick += MainMenu_OnPlayButtonClick;
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
    }

    private IEnumerator AnimationHideDelay() {
        yield return new WaitForSeconds(0.5f);

        HideImmediately();
    }


}
