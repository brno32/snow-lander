using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Text difficultyDisplayText;

    private string easy = "Difficulty: Easy";
    private string hard = "Difficulty: Hard";

    public void Start()
    {
        if (DifficultyTracker.isEasy)
        {
            difficultyDisplayText.text = easy;
        }
        else
        {
            difficultyDisplayText.text = hard;
        }
    }

    public void OptionsButton()
    {
        DifficultyTracker.isEasy = !DifficultyTracker.isEasy;

        if (DifficultyTracker.isEasy)
        {
            difficultyDisplayText.text = easy;
        }
        else
        {
            difficultyDisplayText.text = hard;
        }
    }
}
