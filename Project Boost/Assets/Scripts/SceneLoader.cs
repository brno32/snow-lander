using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public float loadLevelDelay = 1.25f;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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
            print("No more levels. You win!");
        }
    }

    // Initializing this value as a variable was causing it to fail to be updated
    private int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
