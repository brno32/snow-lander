using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour {

    public GameObject[] buttons;

    // Update is called once per frame
    void Update () {
        foreach (GameObject button in buttons)
        {
            if (GameMaster.currentGameState == GameMaster.GameState.Paused)
            {
                button.SetActive(false);
            }
            else if (GameMaster.currentGameState != GameMaster.GameState.Paused)
            {
                button.SetActive(true);
            }
        }
	}
}
