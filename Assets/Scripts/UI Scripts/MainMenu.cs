using System;
using UnityEngine;
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
    [SerializeField] private Button PlayerPrefDeleteButton;

    private void Awake() {
        Instance = this;

        Time.timeScale = 1f;

        PlayButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            OnPlayButtonClick?.Invoke(this, EventArgs.Empty);

        });

        OptionsButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            OnOptionsButtonClick?.Invoke(this, EventArgs.Empty);
        });

        QuitButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayButtonTapSound();

            Application.Quit();
        });

        PlayerPrefDeleteButton.onClick.AddListener(() => {
            PlayerPrefs.DeleteAll();

            Application.Quit();
        });
    }
}
