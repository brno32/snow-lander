using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    
    public SceneLoader sceneLoader;

    // UI Elements
    public GameObject optionsMenu;
    public GameObject pauseMenu;

    private float delay;

    private void Start()
    {
        // Leave UI elements unchecked/checked freely in editor
        DisableOptions();
        UnpauseGame();
    }

    private void Update()
    {
        if (GameMaster.currentGameState == GameMaster.GameState.Transcending ||
            GameMaster.currentGameState == GameMaster.GameState.Dead ||
            GameMaster.currentGameState == GameMaster.GameState.Win)
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    // Resume (UI button) and Cancel (Controller button) share this method
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

        EventSystem.current.SetSelectedGameObject(null);
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
        GameMaster.ChangeGameState(GameMaster.GameState.MainMenu);
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
