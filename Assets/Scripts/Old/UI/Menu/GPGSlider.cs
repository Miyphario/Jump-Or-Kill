using UnityEngine;
using UnityEngine.UI;

public class GPGSlider : MonoBehaviour
{
    public Slider slider;

    public void UpdateSliderValue(bool authorized)
    {
        if (authorized)
        {
            slider.value = 1;
        }
        else
        {
            slider.value = 0;
        }
    }

    public void SliderValueChanged()
    {
        if (slider.value == 1)
        {
            PlayGamesScript.SignIn();
        }
        else
        {
            PlayGamesScript.SignOut();
        }
    }
}
