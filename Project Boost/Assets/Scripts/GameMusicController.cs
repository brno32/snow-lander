using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicController : MonoBehaviour {

    private void Awake()
    {
        int numOfSongs = FindObjectsOfType<GameMusicController>().Length;

        if (numOfSongs > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameMaster.currentGameState == GameMaster.GameState.MainMenu)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
