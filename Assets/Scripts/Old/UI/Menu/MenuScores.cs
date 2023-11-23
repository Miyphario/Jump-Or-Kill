using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScores : MonoBehaviour
{
    public GameObject my_scores, main_scores;

    public void ToMain()
    {
        if (my_scores.activeSelf)
            my_scores.SetActive(false);

        if (!main_scores.activeSelf)
            main_scores.SetActive(true);
    }

    public void ToScores()
    {
        if (main_scores.activeSelf)
            main_scores.SetActive(false);

        if (!my_scores.activeSelf)
            my_scores.SetActive(true);
    }

    public void ShowLeaders()
    {
        PlayGamesScript.ShowLeaderboarsUI();
    }

    public void ShowAchievements()
    {
        PlayGamesScript.ShowAchievementUI();
    }
}
