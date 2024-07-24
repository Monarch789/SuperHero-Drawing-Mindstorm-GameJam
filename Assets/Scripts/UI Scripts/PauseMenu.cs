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


    public event EventHandler OnPauseButtonClick;
    public event EventHandler OnPlayButtonClick;

    [SerializeField] private GameObject pauseMenuUI;


    private void Awake() {
        Instance = this;

        pauseButton.onClick.AddListener(()=> { 
            OnPauseButtonClick?.Invoke(this, EventArgs.Empty);

            //enable the play buttons and other stuff and disable pause button
            pauseMenuUI.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        });
        playButton.onClick.AddListener(() => {
            OnPlayButtonClick?.Invoke(this, EventArgs.Empty);

            //disable the play buttons and other stuff and enable pause button
            pauseMenuUI.SetActive(false);
            pauseButton.gameObject.SetActive(true);

        });
    }

    private void Start() {
        //disable the pause menu at first
        pauseMenuUI.SetActive(false);
    }
}