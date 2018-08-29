using UnityEngine;
using UnityEngine.UI;

public interface OptionsProtocol {
    void LeaveOptionsMenu();
};

public interface MobileProtocol
{
    void UpdateRotateControls(int selectedRotateControls);
}

public class OptionsMenu : MonoBehaviour {

    // UI Text References
    public Text difficultyDisplayText;
    public Text qualityDisplayText;
    public Text soundPreferenceText;
    
    private string easy = "Difficulty: Easy";
    private string hard = "Difficulty: Hard";
    
    private string low = "Quality: Low";
    private string medium = "Quality: Medium";
    private string high = "Quality: High";

    private string soundOn = "Sound: On";
    private string soundOff = "Sound: Off";

    // Saved Data References
    private int selectedDifficulty;
    private int selectedQualityLvl;
    private int selectedSoundPreference;

    // Delegate
    public MainMenu mainMenu;
    OptionsProtocol mainMenuDelegate;

    // Delegate
    public PauseMenu pauseMenu;
    OptionsProtocol pauseMenuDelegate;

    public GameObject snowEffect;

    private void Start()
    {
        selectedDifficulty = PlayerPrefs.GetInt("difficulty", 1);
        selectedQualityLvl = PlayerPrefs.GetInt("quality", 2);
        selectedSoundPreference = PlayerPrefs.GetInt("sound", 1);

        UpdateDifficulty();
        UpdateQuality();
        UpdateSound();

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

        UpdateDifficulty();
    }

    // Button
    public void ToggleSound()
    {
        if (selectedSoundPreference == 0)
        {
            selectedSoundPreference = 1;
        }
        else
        {
            selectedSoundPreference = 0;
        }

        UpdateSound();
    }

    private void UpdateSound()
    {
        if (selectedSoundPreference != 0)
        {
            AudioListener.volume = 1f;
            soundPreferenceText.text = soundOn;
        }
        else
        {
            AudioListener.volume = 0f;
            soundPreferenceText.text = soundOff;
        }

        PlayerPrefs.SetInt("sound", selectedSoundPreference);
    }

    private void UpdateDifficulty()
    {
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

        if (snowEffect != null)
        {
            if (QualitySettings.GetQualityLevel() == 0)
            {
                snowEffect.SetActive(false);
            }
            else
            {
                snowEffect.SetActive(true);
            }
        }

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
        
        switch (GetNormalizedQualityIndex())
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
