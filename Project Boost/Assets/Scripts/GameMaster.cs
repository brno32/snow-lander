using UnityEngine;

public class GameMaster : MonoBehaviour {

    [Header("External Components")]
    public Rocket rocket;
    public SceneLoader sceneLoader;

    // STATE VARIABLES
    [HideInInspector] public enum GameState { Alive, Transcending, Dead, Paused };
    [HideInInspector] static public GameState currentGameState;
    
    void Start () {
        currentGameState = GameState.Alive;
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
            default:
                break;
        }
    }

    public static void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
    }
}
