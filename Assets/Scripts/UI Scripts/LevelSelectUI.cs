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


    //aniamtor 
    private Animator animator;


    private const string TutorialSteps = "TutorialSteps";

    private void Awake() {
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
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });
        Level2Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });
        Level3Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });
        Level4Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });
        Level5Button.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });


    }

    private void Start() {
        Hide();

        MainMenu.Instance.OnPlayButtonClick += MainMenu_OnPlayButtonClick;
    }

    private void MainMenu_OnPlayButtonClick(object sender, System.EventArgs e) {
        Show();

    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    private void Show() {
        gameObject.SetActive(true);

        animator.SetTrigger("OnPlayClick");
    }

    private void OnDestroy() {
        MainMenu.Instance.OnPlayButtonClick -= MainMenu_OnPlayButtonClick;
    }


}
