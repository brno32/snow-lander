﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MobileControlRig provides useful functionality, but makes it impossible to hide
// its UI elements. Rather than step on its toes, attach this script to mobile controls
// so they properly disappear when the game is paused
public class MobileControlRigKiller : MonoBehaviour {

    private Text text;
    private Image uiLabel;
    private Slider slider;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
        uiLabel = gameObject.GetComponent<Image>();
        slider = gameObject.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {

		if (GameMaster.currentGameState == GameMaster.GameState.Paused)
        {
            CheckText(false);
            CheckUILabel(false);
            CheckSlider(false);
        }
        else
        {
            CheckText(true);
            CheckUILabel(true);
            CheckSlider(true);
        }
	}

    void CheckText(bool isEnabled)
    {
        if (text != null)
        {
            text.enabled = isEnabled;
        }
    }

    void CheckUILabel(bool isEnabled)
    {
        if (uiLabel != null)
        {
            uiLabel.enabled = isEnabled;
        }
    }

    void CheckSlider(bool isEnabled)
    {
        if (slider != null)
        {
            slider.enabled = isEnabled;
            print(slider.enabled);
        }
    }
}
