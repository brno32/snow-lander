using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {

    public Rocket rocket;

    public SceneLoader sceneLoader;

    public Canvas optionsMenu;
    public Canvas pauseMenu;

    private float delay;

    private void Start()
    {
        DisableOptions();
        DisablePauseMenu();
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (optionsMenu.enabled)
        {
            DisableOptions();
            DisablePauseMenu();
        }
        else if (!pauseMenu.enabled)
        {
            EnablePauseMenu();
        }
        else
        {
            DisablePauseMenu();
        }
    }

    public void GoToOptionsMenu()
    {
        EnableOptions();
        DisablePause();
    }

    public void GoBackToPauseMenu()
    {
        DisableOptions();
        EnablePause();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void DisableOptions()
    {
        optionsMenu.enabled = false;
    }

    private void EnableOptions()
    {
        optionsMenu.enabled = true;
    }

    private void DisablePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.enabled = false;
        GameMaster.ChangeGameState(GameMaster.GameState.Alive);
    }

    private void EnablePause()
    {
        pauseMenu.enabled = true;
    }

    private void DisablePause()
    {
        pauseMenu.enabled = false;
    }

    private void EnablePauseMenu()
    {
        pauseMenu.enabled = true;
        GameMaster.ChangeGameState(GameMaster.GameState.Paused);
        rocket.PauseEffects();
        Time.timeScale = 0f;
    }
}
