using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    [Header("External Components")]
    public Rocket rocket;
    public SceneLoader sceneLoader;
    public GameObject winMessage;
    public Text levelDisplay;

    // SCENE MANAGEMENT VARIABLES
    [HideInInspector] static public int numOfLevels;
    [HideInInspector] static public int currentLevel;

    // STATE VARIABLES
    [HideInInspector] public enum GameState { MainMenu, Alive, Transcending, Dead, Paused, Win };
    [HideInInspector] static public GameState currentGameState;
    
    void Start () {
        ChangeGameState(GameState.Alive);
        winMessage.SetActive(false);

        // Don't count main menu as a level
        numOfLevels = SceneManager.sceneCountInBuildSettings - 1;
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        
        PlayerPrefs.SetInt("level", currentLevel);

        if (levelDisplay != null)
        {
            levelDisplay.text = currentLevel + "/" + numOfLevels;
        }
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
                // clear player's cached current level if finishing game
                PlayerPrefs.SetInt("level", 100);
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
