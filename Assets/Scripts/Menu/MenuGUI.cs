using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGUI : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject shop_menu;
    public GameObject settigs_menu;

    public PlayerData playerData;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {

    }

    public void OpenSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
