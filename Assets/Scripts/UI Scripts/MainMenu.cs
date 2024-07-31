using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    //Singleton
    public static MainMenu Instance {  get; private set; }


    //event to send level select to show itself
    public event EventHandler OnPlayButtonClick;
    public event EventHandler OnOptionsButtonClick;

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button QuitButton;

    private void Awake() {
        Instance = this;

        PlayButton.onClick.AddListener(() => {

            OnPlayButtonClick?.Invoke(this, EventArgs.Empty);

        });

        OptionsButton.onClick.AddListener(() => {
            OnOptionsButtonClick?.Invoke(this, EventArgs.Empty);
        });

        QuitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

}
