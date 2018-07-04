using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public SceneLoader sceneLoader;

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        sceneLoader.BeginLoadingNextScene(true);
        animator.enabled = true;
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
