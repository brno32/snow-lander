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
        SceneManager.LoadScene(0);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(GetCurrentScene());
    }

    private void LoadNextScene()
    {
        if (GetCurrentScene() < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(GetCurrentScene() + 1);
        }
        else
        {
            GameMaster.ChangeGameState(GameMaster.GameState.Win);
        }
    }

    // Initializing this value as a variable was causing it to fail to be updated
    private int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
