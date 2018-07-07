using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public SceneLoader sceneLoader;
    public GameObject mainMenuRocket;

    public GameObject optionsMenu;
    
    void Start () {
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

    private void DisableOptions()
    {
        optionsMenu.SetActive(false);
    }

    private void EnableOptions()
    {
        optionsMenu.SetActive(true);
    }

    private void DisableMainMenu()
    {
        mainMenu.SetActive(false);
        mainMenuRocket.SetActive(false);
    }

    private void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        mainMenuRocket.SetActive(true);
    }
}
