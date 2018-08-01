using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicController : MonoBehaviour {

    private void Awake()
    {
        int numOfSongsPlaying = FindObjectsOfType<GameMusicController>().Length;

        if (numOfSongsPlaying > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameMaster.currentGameState == GameMaster.GameState.Win ||
            GameMaster.currentGameState == GameMaster.GameState.MainMenu)
        {
            // Kill the music if we're about to load the main menu
            gameObject.SetActive(false);
            Destroy(gameObject);
            print("destroyed");
        }
    }
}
