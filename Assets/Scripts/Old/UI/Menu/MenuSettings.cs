using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public GPGSlider gpg_slider;

    private void OnEnable()
    {
        if (gpg_slider != null)
        {
            if (PlayGamesScript.GetAuthorized())
            {
                gpg_slider.UpdateSliderValue(true);
            }
            else
            {
                gpg_slider.UpdateSliderValue(false);
            }
        }
    }
}
