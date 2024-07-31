using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum GameScenes {
        MainMenu,
        SampleScene,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
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
        LoadScene(currentScene);
    }

    public static GameScenes GetCurrentScene() {
        return currentScene;
    }

    public static void LoadNextLevel() {
        if(currentScene == GameScenes.Level1) {
            LoadScene(GameScenes.Level2);
        }
        else if(currentScene == GameScenes.Level2) {
            LoadScene(GameScenes.Level3);
        }
        else if (currentScene == GameScenes.Level3) {
            LoadScene(GameScenes.Level4);
        }
        else if (currentScene == GameScenes.Level4) {
            LoadScene(GameScenes.Level5);
        }
        else if (currentScene == GameScenes.Level5) {
            LoadScene(GameScenes.MainMenu);
        }
    }
}
