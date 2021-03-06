﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, OptionsProtocol
{
    // For returning to main menu
    public SceneLoader sceneLoader;

    // UI Elements
    public GameObject optionsMenu;
    public GameObject pauseMenu;

    private void Start()
    {
        // Leave UI elements unchecked/checked freely in editor
        DisableOptions();
        UnpauseGame();
    }

    private void Update()
    {
        if ( IsTranscending() || IsDead() || IsWinning() )
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
        if (optionsMenu == null || pauseMenu == null)
        {
            return;
        }

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

    void OptionsProtocol.LeaveOptionsMenu()
    {
        DisableOptions();
        EnablePause();
    }

    // Button
    public void GoToOptionsMenu()
    {
        EnableOptions();
        DisablePause();

        EventSystem.current.SetSelectedGameObject(null);
    }

    // Button
    public void GoToMainMenu()
    {
        GameMaster.ChangeGameState(GameMaster.GameState.MainMenu);
        sceneLoader.LoadMainMenu();
    }

    private void EnableOptions()
    {
        if (optionsMenu == null)
        {
            return;
        }

        optionsMenu.SetActive(true);
    }

    private void DisableOptions()
    {
        if (optionsMenu == null)
        {
            return;
        }

        optionsMenu.SetActive(false);
    }

    private void PauseGame()
    {
        EnablePause();
        GameMaster.ChangeGameState(GameMaster.GameState.Paused);
    }

    private void UnpauseGame()
    {
        DisablePause();
        GameMaster.ChangeGameState(GameMaster.GameState.Alive);
    }

    private void EnablePause()
    {
        if (pauseMenu == null)
        {
            return;
        }

        pauseMenu.SetActive(true);
    }

    private void DisablePause()
    {
        if (pauseMenu == null)
        {
            return;
        }

        pauseMenu.SetActive(false);
    }

    private bool IsDead()
    {
        return GameMaster.currentGameState == GameMaster.GameState.Dead;
    }

    private bool IsTranscending()
    {
        return GameMaster.currentGameState == GameMaster.GameState.Transcending;
    }

    private bool IsWinning()
    {
        return GameMaster.currentGameState == GameMaster.GameState.Win;
    }
}
