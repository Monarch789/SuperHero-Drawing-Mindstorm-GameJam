using System;
using System.Collections;
using System.Collections.Generic;
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
            OnPauseButtonClick?.Invoke(this, EventArgs.Empty);

            //enable the play buttons and other stuff and disable pause button
            pauseMenuUI.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            animator.SetTrigger("OnPause");
        });
        playButton.onClick.AddListener(() => {
            OnPlayButtonClick?.Invoke(this, EventArgs.Empty);

            //disable the play buttons and other stuff and enable pause button
            pauseMenuUI.SetActive(false);
            pauseButton.gameObject.SetActive(true);

        });

        retryButton.onClick.AddListener(() => {
            Loader.LoadCurrentScene();
        });
        menuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.MainMenu);
        });
    }

    private void Start() {
        //disable the pause menu at first
        pauseMenuUI.SetActive(false);
    }
}
