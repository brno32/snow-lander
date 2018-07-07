using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {

    public Rocket rocket;

    public SceneLoader sceneLoader;

    public GameObject optionsMenu;
    public GameObject pauseMenu;

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

        print(pauseMenu.activeSelf);
    }

    public void Toggle()
    {
        if (optionsMenu.activeSelf)
        {
            DisableOptions();
            DisablePauseMenu();
        }
        else if (!pauseMenu.activeSelf)
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

    public void GoToPauseMenu()
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
        optionsMenu.SetActive(false);
    }

    private void EnableOptions()
    {
        optionsMenu.SetActive(true);
    }

    private void DisablePauseMenu()
    {
        Time.timeScale = 1f;
        print(pauseMenu.activeSelf);
        pauseMenu.SetActive(false);
        GameMaster.ChangeGameState(GameMaster.GameState.Alive);
    }

    private void EnablePause()
    {
        pauseMenu.SetActive(true);
    }

    private void DisablePause()
    {
        pauseMenu.SetActive(false);
        print(Time.timeScale);
    }

    private void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        print(pauseMenu.activeSelf);
        GameMaster.ChangeGameState(GameMaster.GameState.Paused);
        rocket.PauseEffects();
        Time.timeScale = 0f;
    }
}
