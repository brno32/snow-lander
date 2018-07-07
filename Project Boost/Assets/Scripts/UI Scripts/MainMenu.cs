using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [Tooltip("Needs a reference to a game object with SceneLoader attached")]
    public SceneLoader sceneLoader;
    
    public GameObject mainMenuRocket;

    [Tooltip("Needs a reference to the panel that contains the Options Menu UI elements")]
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
        gameObject.SetActive(false);
        mainMenuRocket.SetActive(false);
    }

    private void EnableMainMenu()
    {
        gameObject.SetActive(true);
        mainMenuRocket.SetActive(true);
    }
}
