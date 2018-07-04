using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public SceneLoader sceneLoader;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        sceneLoader.BeginLoadingNextScene(true);
    }

    public void Options()
    {
        Debug.Log("Options pressed...");
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
