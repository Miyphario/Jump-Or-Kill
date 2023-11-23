using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image background;
    public float currentAlpha = 0f;

    public GameHUD gameHud;

    private void Awake()
    {
        background = GetComponent<Image>();

        // Set background alpha to 0
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
    }

    private void OnEnable()
    {
        // If menu enabled increase background alpha
        currentAlpha = 0.35f;
        StartCoroutine(UpdateAlpha());
    }

    private void OnDisable()
    {
        // If menu disabled set background alpha to 0
        currentAlpha = 0f;
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0f);
    }

    public IEnumerator UpdateAlpha()
    {
        // Alpha animation
        float al = background.color.a;
        while(al < currentAlpha)
        {
            al += 0.02f;
            background.color = new Color(background.color.r, background.color.g, background.color.b, al);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        
    }
}
