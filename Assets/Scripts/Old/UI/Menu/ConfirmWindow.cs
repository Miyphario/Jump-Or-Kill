using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
public enum ConfirmType
{
    reset_progress,
    reset_scores,
    quit,
    in_menu,
    game_restart
}

public class ConfirmWindow : MonoBehaviour
{
    public Text info_text;
    public GameObject object_to_activate;
    public MenuHUD menu_hud;
    public HUD game_hud;
    public bool in_menu = true;

    private ConfirmType type;

    public void ShowWindow(string text, ConfirmType type)
    {
        info_text.text = text;
        this.type = type;
    }

    public void HideWindow()
    {
        if (!object_to_activate.activeSelf)
            object_to_activate.SetActive(true);

        if (in_menu)
        {
            menu_hud.ToSelectMenu(menu_hud.selected_menu);
        }

        gameObject.SetActive(false);
    }

    public void ClickedOnConfirm()
    {
        switch (type)
        {
            case ConfirmType.reset_progress:
                SaveSystem.ResetGameProgress();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case ConfirmType.reset_scores:
                SaveSystem.ResetScores();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case ConfirmType.quit:
                Application.Quit();
                break;

            case ConfirmType.in_menu:
                game_hud.TogglePause();
                SceneManager.LoadScene(0);
                break;

            case ConfirmType.game_restart:
                game_hud.TogglePause();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf)
                HideWindow();
        }
    }
}
*/