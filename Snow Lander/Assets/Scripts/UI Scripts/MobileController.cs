using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour
{
    private Text text;
    private Image uiLabel;
    
    private List<GameObject> buttons = new List<GameObject>();

    void Update()
    {
        foreach (GameObject btn in buttons)
        {
            Text text = btn.GetComponent<Text>();
            Image uiLabel = btn.GetComponent<Image>();

            bool isEnabled = true;

            if (GameMaster.currentGameState == GameMaster.GameState.Paused)
            {
                isEnabled = false;
            }

            CheckText(text, isEnabled);
            CheckUILabel(uiLabel, isEnabled);
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

    void CheckText(Text text, bool _isEnabled)
    {
        if (text != null)
        {
            text.enabled = _isEnabled;
        }
    }

    void CheckUILabel(Image uiLabel, bool _isEnabled)
    {
        if (uiLabel != null)
        {
            uiLabel.enabled = _isEnabled;
        }
    }
}