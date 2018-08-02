using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, OptionsProtocol
{
    
    public SceneLoader sceneLoader;
    public GameObject mainMenuRocket;
    public GameObject optionsMenu;
    
    void Start () {
        // Leave UI elements unchecked/checked freely in editor
        EnableMainMenu();
        DisableOptions();
    }

    // Button
    public void Play()
    {
        sceneLoader.BeginLoadingNextScene(true);
    }

    // Button
    public void Options()
    {
        EnableOptions();
        DisableMainMenu();
    }

    void OptionsProtocol.LeaveOptionsMenu()
    {
        Back();
    }

    // Button
    public void Back()
    {
        DisableOptions();
        EnableMainMenu();
    }

    // Button
    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    private void EnableOptions()
    {
        optionsMenu.SetActive(true);
    }

    private void DisableOptions()
    {
        optionsMenu.SetActive(false);
    }

    private void EnableMainMenu()
    {
        gameObject.SetActive(true);
        mainMenuRocket.SetActive(true);
    }

    private void DisableMainMenu()
    {
        gameObject.SetActive(false);
        mainMenuRocket.SetActive(false);
    }
}
