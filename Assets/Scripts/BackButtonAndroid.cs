using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonAndroid : MonoBehaviour{

    private void Start() {
        InputManager.Instance.OnEscaePressed += InputManager_OnEscaePressed;
    }

    private void InputManager_OnEscaePressed(object sender, System.EventArgs e) {
        if(Application.platform == RuntimePlatform.Android) {
            //only work on android
            if(Loader.GetCurrentScene() == Loader.GameScenes.MainMenu) {
                Application.Quit();
            }
            else {
                Loader.LoadScene(Loader.GameScenes.MainMenu);
            }
        }
        Debug.Log("Back Button Pressed");
    }

    private void OnDestroy() {
        InputManager.Instance.OnEscaePressed -= InputManager_OnEscaePressed;
    }
}
