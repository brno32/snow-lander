using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public float loadLevelDelay = 1.25f;

    public void BeginLoadingCurrentScene()
    {
        Invoke("LoadCurrentScene", loadLevelDelay);
    }

    public void BeginLoadingNextScene()
    {
        Invoke("LoadNextScene", loadLevelDelay);
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
