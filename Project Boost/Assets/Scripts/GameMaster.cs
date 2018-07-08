using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    [Header("External Components")]
    public Rocket rocket;
    public SceneLoader sceneLoader;
    public GameObject winMessage;
    public Text levelDisplay;

    private int numOfLevels;
    private int currentLevel;

    // STATE VARIABLES
    [HideInInspector] public enum GameState { MainMenu, Alive, Transcending, Dead, Paused, Win };
    [HideInInspector] static public GameState currentGameState;
    
    void Start () {
        ChangeGameState(GameState.Alive);
        winMessage.SetActive(false);

        if (levelDisplay == null)
        {
            return;
        }

        numOfLevels = SceneManager.sceneCountInBuildSettings - 1;
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        levelDisplay.text = currentLevel + "/" + numOfLevels;
    }

    void Update() {
        switch (currentGameState)
        {
            case GameState.Alive:
                Time.timeScale = 1f;
                return; 
            case GameState.Transcending:
                sceneLoader.BeginLoadingNextScene();
                break;
            case GameState.Dead:
                sceneLoader.BeginLoadingCurrentScene();
                break;
            case GameState.Paused:
                rocket.PauseEffects();
                Time.timeScale = 0f;
                break;
            case GameState.Win:
                winMessage.SetActive(true);
                sceneLoader.BeginLoadingMainMenu();
                break;
            default:
                break;
        }
    }

    public static void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
    }
}
