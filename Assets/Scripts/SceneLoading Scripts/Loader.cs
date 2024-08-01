
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
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
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
            LoadScene(GameScenes.Level6);
        }
        else if (currentScene == GameScenes.Level6) {
            LoadScene(GameScenes.Level7);
        }
        else if (currentScene == GameScenes.Level7) {
            LoadScene(GameScenes.Level8);
        }
        else if (currentScene == GameScenes.Level8) {
            LoadScene(GameScenes.Level9);
        }
        else if (currentScene == GameScenes.Level9) {
            LoadScene(GameScenes.Level10);
        }
        else if (currentScene == GameScenes.Level10) {
            LoadScene(GameScenes.Level11);
        }
        else if (currentScene == GameScenes.Level11) {
            LoadScene(GameScenes.Level12);
        }
        else if (currentScene == GameScenes.Level12) {
            LoadScene(GameScenes.MainMenu);
        }
    }
}
