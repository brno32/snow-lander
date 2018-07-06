using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public Canvas mainMenu;
    public SceneLoader sceneLoader;
    public GameObject mainMenuRocket;

    public Canvas optionsMenu;
    
    void Start () {
        EnableMainMenu();
        DisableOptions();
    }

    public void Play()
    {
        sceneLoader.BeginLoadingNextScene(true);
    }

    public void Options()
    {
        EnableOptions();
        DisableMainMenu();
    }

    public void Back()
    {
        DisableOptions();
        EnableMainMenu();
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    private void DisableOptions()
    {
        optionsMenu.enabled = false;
    }

    private void EnableOptions()
    {
        optionsMenu.enabled = true;
    }

    private void DisableMainMenu()
    {
        mainMenu.enabled = false;
        mainMenuRocket.SetActive(false);
        mainMenuRocket.SetActive(false);
    }

    private void EnableMainMenu()
    {
        mainMenu.enabled = true;
        mainMenuRocket.SetActive(true);
        mainMenuRocket.SetActive(true);
    }
}
