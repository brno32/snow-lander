using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    // Button
    public void Continue()
    {
        int lastPlayedLevel = PlayerPrefs.GetInt("level");
        SceneManager.LoadScene(lastPlayedLevel);
    }

    // Button
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
