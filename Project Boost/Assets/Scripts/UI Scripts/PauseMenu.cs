using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {
    
    public SceneLoader sceneLoader;

    // UI Elements
    public GameObject optionsMenu;
    public GameObject pauseMenu;

    private float delay;

    private void Start()
    {
        DisableOptions();
        UnpauseGame();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    // Resume Button and Update share this method
    public void TogglePause()
    {
        if (optionsMenu.activeSelf)
        {
            DisableOptions();
            UnpauseGame();
        }
        else if (!pauseMenu.activeSelf)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    // Button
    public void GoToOptionsMenu()
    {
        EnableOptions();
        DisablePause();
    }

    // Button
    public void GoToPauseMenu()
    {
        DisableOptions();
        EnablePause();
    }

    // Button
    public void GoToMainMenu()
    {
        sceneLoader.LoadMainMenu();
    }

    private void DisableOptions()
    {
        optionsMenu.SetActive(false);
    }

    private void EnableOptions()
    {
        optionsMenu.SetActive(true);
    }

    private void UnpauseGame()
    {
        DisablePause();
        GameMaster.ChangeGameState(GameMaster.GameState.Alive);
    }

    private void EnablePause()
    {
        pauseMenu.SetActive(true);
    }

    private void DisablePause()
    {
        pauseMenu.SetActive(false);
    }

    private void PauseGame()
    {
        EnablePause();
        GameMaster.ChangeGameState(GameMaster.GameState.Paused);
    }
}
