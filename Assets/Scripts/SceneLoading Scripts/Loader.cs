using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum GameScenes {
        MainMenu,
        SampleScene,
        LoadingScene,
        TutorialScene,
    }

    private static GameScenes targetScene;

    private static GameScenes currentScene = GameScenes.MainMenu;

    public static void LoadScene(GameScenes scene) {
        targetScene = scene;

        SceneManager.LoadScene(GameScenes.LoadingScene.ToString());
    }

    public static void LoaderCallBack() {
        currentScene = targetScene;

        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void LoadCurrentScene() {

        Loader.LoadScene(currentScene);
    }
}
