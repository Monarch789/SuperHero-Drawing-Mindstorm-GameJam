using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    private void Awake() {
        PlayButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.GameScenes.SampleScene);
        });

        QuitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

}
