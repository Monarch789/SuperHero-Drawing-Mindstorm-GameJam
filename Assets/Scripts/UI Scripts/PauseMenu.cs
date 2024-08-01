using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    //Sigleton
    public static PauseMenu Instance { get; private set; }
    
    // References of buttons
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;


    public event EventHandler OnPauseButtonClick;
    public event EventHandler OnPlayButtonClick;

    [SerializeField] private GameObject pauseMenuUI;

    private Animator animator;


    private void Awake() {
        Instance = this;

        animator = GetComponent<Animator>();

        pauseButton.onClick.AddListener(()=> {
            SoundManager.Instance.PlayButtonTapSound();

            OnPauseButtonClick?.Invoke(this, EventArgs.Empty);

            //enable the play buttons and other stuff and disable pause button
            pauseMenuUI.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            animator.SetTrigger("OnPause");
        });
        playButton.onClick.AddListener(() => {
            OnPlayButtonClick?.Invoke(this, EventArgs.Empty);

            SoundManager.Instance.PlayButtonTapSound();

            //disable the play buttons and other stuff and enable pause button
            pauseMenuUI.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        });

        retryButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadCurrentScene();
        });
        menuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Loader.LoadScene(Loader.GameScenes.MainMenu);
        });
    }

    private void Start() {
        //disable the pause menu at first
        pauseMenuUI.SetActive(false);
    }
    
}
