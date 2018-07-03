using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public Rocket rocket;

    public enum GameState { Alive, Transcending, Dead };
    static public GameState currentGameState;

    private float loadLevelDelay = 1f;

    // Use this for initialization
    void Start () {
        currentGameState = GameState.Alive;
    }

    void Update() {
        switch (currentGameState)
        {
            case GameState.Alive:
                return; 
            case GameState.Transcending:
                BeginLoadingNextScene();
                break;
            case GameState.Dead:
                BeginLoadingCurrentScene();
                break;
            default:
                break;
        }
    }

    public void BeginLoadingCurrentScene()
    {
        Invoke("LoadCurrentScene", loadLevelDelay);
    }

    public void BeginLoadingNextScene()
    {
        print("BeginLoadingNextScene");
        Invoke("LoadNextScene", loadLevelDelay);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(GetCurrentScene());
    }

    public void LoadNextScene()
    {
        print("LoadNextScene");
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
