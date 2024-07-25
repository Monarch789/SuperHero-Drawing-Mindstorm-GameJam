using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum GameScenes {
        MainMenu,
        SampleScene,
        LoadingScene
    }

    private static GameScenes targetScene;

    public static void LoadScene(GameScenes scene) {
        targetScene = scene;

        SceneManager.LoadScene(GameScenes.LoadingScene.ToString());

    }

    public static void LoaderCallBack() {

        SceneManager.LoadScene(targetScene.ToString());
    }

}
