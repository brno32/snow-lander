using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, MobileProtocol
{
    private Text text;
    private Image uiLabel;
    private Slider slider;

    // private GameObject[] buttons;
    private List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            GameObject mobileBtn = transform.GetChild(index).gameObject;
            buttons.Add(mobileBtn);
        }
    }

    void Update()
    {
        foreach (GameObject btn in buttons)
        {
            Text text = btn.GetComponent<Text>();
            Image uiLabel = btn.GetComponent<Image>();
            Slider slider = btn.GetComponent<Slider>();

            bool isEnabled = true;

            if (GameMaster.currentGameState == GameMaster.GameState.Paused)
            {
                isEnabled = false;
            }

            CheckText(text, isEnabled);
            CheckUILabel(uiLabel, isEnabled);
            CheckSlider(slider, isEnabled);
        }
    }

    void MobileProtocol.UpdateRotateControls(int selectedRotateControls)
    {
         
        if (selectedRotateControls == 0)
        {
            //slider.SetActive(false);
            //rotateBtn.SetActive(true);
        }
        else
        {
            //slider.SetActive(true);
            //rotateBtn.SetActive(false);
        }
    }

    void CheckText(Text text, bool isEnabled)
    {
        if (text != null)
        {
            text.enabled = isEnabled;
        }
    }

    void CheckUILabel(Image uiLabel, bool isEnabled)
    {
        if (uiLabel != null)
        {
            uiLabel.enabled = isEnabled;
        }
    }

    void CheckSlider(Slider slider, bool isEnabled)
    {
        if (slider != null)
        {
            slider.enabled = isEnabled;
        }
    }
}