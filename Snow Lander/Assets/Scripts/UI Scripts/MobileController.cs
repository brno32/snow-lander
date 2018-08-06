using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, MobileProtocol
{
    private Text text;
    private Image uiLabel;
    private Slider slider;

    private bool useSlider = false;
    
    private List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        GatherMobileButtons(transform);
        foreach (GameObject btn in buttons)
        {
            print(btn);
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

            //if (btn.tag == "Slider")
            //{
            //    if (btn.activeSelf)
            //    {
            //        CheckText(text, useSlider);
            //        CheckUILabel(uiLabel, useSlider);
            //        CheckSlider(slider, useSlider);
            //    }
            //}
            //if (btn.tag == "Rotate")
            //{
            //    if (btn.activeSelf)
            //    {
            //        CheckText(text, !useSlider);
            //        CheckUILabel(uiLabel, !useSlider);
            //        CheckSlider(slider, !useSlider);
            //    }
            //}
        }
    }

    void MobileProtocol.UpdateRotateControls(int selectedRotateControls)
    {
         
        if (selectedRotateControls == 0)
        {
            useSlider = false;
        }
        else
        {
            useSlider = true;
        }
    }

    void GatherMobileButtons(Transform _transform)
    {
        for (int index = 0; index < _transform.childCount; index++)
        {
            Transform mobileBtn = _transform.GetChild(index);
            buttons.Add(mobileBtn.gameObject);
            GatherMobileButtons(mobileBtn);
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

    void CheckSlider(Slider slider,  bool isEnabled)
    {
        if (slider != null)
        {
            slider.enabled = isEnabled;
        }
    }
}