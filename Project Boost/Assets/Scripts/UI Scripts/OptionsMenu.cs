using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour {

    public Text difficultyDisplayText;

    private string easy = "Difficulty: Easy";
    private string hard = "Difficulty: Hard";

    private void Start()
    {
        SetDifficultyText();
    }

    public void ChangeDifficulty()
    {
        DifficultyTracker.isEasy = !DifficultyTracker.isEasy;
        SetDifficultyText();
    }

    public void SetDifficultyText()
    {
        if (difficultyDisplayText == null)
        {
            return;
        }

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
