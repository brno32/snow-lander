using UnityEngine;

public class GameMaster : MonoBehaviour {

    public Rocket rocket;
    public SceneLoader sceneLoader;

    public enum GameState { Alive, Transcending, Dead };
    static public GameState currentGameState;

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
                sceneLoader.BeginLoadingNextScene();
                break;
            case GameState.Dead:
                sceneLoader.BeginLoadingCurrentScene();
                break;
            default:
                break;
        }
    }
}
