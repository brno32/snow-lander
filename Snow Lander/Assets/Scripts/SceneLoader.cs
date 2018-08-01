using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public float loadLevelDelay = 1.25f;
    public float loadMainMenuDelay = 3f;

    public void BeginLoadingMainMenu()
    {
        Invoke("LoadMainMenu", loadMainMenuDelay);
    }

    public void BeginLoadingCurrentScene()
    {
        Invoke("LoadCurrentScene", loadLevelDelay);
    }

    public void BeginLoadingNextScene(bool ignoreDelay = false)
    {
        if (!ignoreDelay)
        {
            Invoke("LoadNextScene", loadLevelDelay);
        }
        else
        {
            LoadNextScene();
        }
    }

    public void LoadMainMenu()
    {
        GameMaster.ChangeGameState(GameMaster.GameState.MainMenu);
        SceneManager.LoadScene(0);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(GameMaster.currentLevel);
    }

    private void LoadNextScene()
    {
        if (GameMaster.currentLevel < GameMaster.numOfLevels)
        {
            SceneManager.LoadScene(GameMaster.currentLevel + 1);
        }
        else
        {
            GameMaster.ChangeGameState(GameMaster.GameState.Win);
        }
    }
}
