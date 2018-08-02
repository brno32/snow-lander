﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface OptionsProtocol {
    void LeaveOptionsMenu();
};

public class OptionsMenu : MonoBehaviour {

    // UI Text References
    public Text difficultyDisplayText;
    public Text qualityDisplayText;
    
    private string easy = "Difficulty: Easy";
    private string hard = "Difficulty: Hard";
    
    private string low = "Quality: Low";
    private string medium = "Quality: Medium";
    private string high = "Quality: High";

    // Saved Data References
    private int selectedDifficulty;
    private int selectedQualityLvl;

    // Delegate
    public MainMenu mainMenu;
    OptionsProtocol mainMenuDelegate;

    // Delegate
    public PauseMenu pauseMenu;
    OptionsProtocol pauseMenuDelegate;

    private void Start()
    {
        selectedDifficulty = PlayerPrefs.GetInt("difficulty", 1);
        selectedQualityLvl = PlayerPrefs.GetInt("quality", 2);
        print(selectedQualityLvl);

        SetDifficultyText();
        UpdateQuality();

        print(qualityDisplayText.text);

        mainMenuDelegate = mainMenu;
        pauseMenuDelegate = pauseMenu;
    }

    // Button
    public void ReturnToMainMenu()
    {
        mainMenuDelegate.LeaveOptionsMenu();
    }

    // Button
    public void ReturnToPause()
    {
        pauseMenuDelegate.LeaveOptionsMenu();
    }

    // Button
    public void ChangeDifficulty()
    {
        if (selectedDifficulty == 1) { selectedDifficulty = 0; }
        else { selectedDifficulty = 1; }

        PlayerPrefs.SetInt("difficulty", selectedDifficulty);
        SetDifficultyText();
    }

    // Button
    public void CycleQuality()
    {
        selectedQualityLvl++;
        UpdateQuality();
    }

    private void UpdateQuality()
    {
        PlayerPrefs.SetInt("quality", GetNormalizedQualityIndex());
        QualitySettings.SetQualityLevel(GetNormalizedQualityIndex());
        SetQualityText();
    }

    public void SetDifficultyText()
    {
        if (difficultyDisplayText == null)
        {
            return;
        }

        if (selectedDifficulty == 0)
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
        return selectedQualityLvl % 3;
    }
}
