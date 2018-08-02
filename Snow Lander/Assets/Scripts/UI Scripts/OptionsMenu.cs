using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface PauseProtocol {
    void GoToPauseMenu();
};

public class OptionsMenu : MonoBehaviour {

    public Text difficultyDisplayText;
    public Text qualityDisplayText;

    private string easy = "Difficulty: Easy";
    private string hard = "Difficulty: Hard";

    private string low = "Quality: Low";
    private string medium = "Quality: Medium";
    private string high = "Quality: High";

    public PauseMenu pauseMenu;
    PauseProtocol pauseMenuDelegate;

    private void Start()
    {
        SetDifficultyText();
        pauseMenuDelegate = pauseMenu;
    }

    // Button
    public void ReturnToPause()
    {
        pauseMenuDelegate.GoToPauseMenu();
    }

    // Button
    public void ChangeDifficulty()
    {
        DifficultyTracker.isEasy = !DifficultyTracker.isEasy;
        SetDifficultyText();
    }

    // Button
    public void CycleQuality()
    {
        QualityTracker.currentQual++;
        QualitySettings.SetQualityLevel(GetNormalizedQualityIndex());
        SetQualityText();
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

    public void SetQualityText()
    {
        if (qualityDisplayText == null)
        {
            return;
        }

        int currentNormalizedQaul = GetNormalizedQualityIndex();
        switch (currentNormalizedQaul)
        {
            case 0:
                qualityDisplayText.text = low;
                break;
            case 1:
                qualityDisplayText.text = medium;
                break;
            case 2:
                qualityDisplayText.text = high;
                break;
            default:
                break;
        }
    }

    int GetNormalizedQualityIndex()
    {
        return QualityTracker.currentQual % 3;
    }
}
