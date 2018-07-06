using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Text difficultyDisplayText;

    public void OptionsButton()
    {
        DifficultyTracker.isEasy = !DifficultyTracker.isEasy;
        difficultyDisplayText.text = "Difficulty: Hard";

        if (DifficultyTracker.isEasy)
        {
            difficultyDisplayText.text = "Difficulty: Easy";
        }
        else
        {
            difficultyDisplayText.text = "Difficulty: Hard";
        }
    }
}
