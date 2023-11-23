using UnityEngine;
using UnityEngine.SceneManagement;

/*
public enum ButtonType
{
    ui,
    play
}

public class GameButtons : MonoBehaviour
{
    public ButtonType button_type = ButtonType.ui;
    public MenuHUD menu_hud;

    public void ShowShop()
    {
        menu_hud.ToSelectMenu(MenuType.shop);
    }

    public void ShowSettings()
    {
        menu_hud.ToSelectMenu(MenuType.settings);
    }

    public void ShowMain()
    {
        menu_hud.ToSelectMenu(MenuType.main);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (button_type == ButtonType.play)
        {
            Invoke("GStart", 0.5f);
        }
    }

    private void GStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResetGameProgress()
    {
        menu_hud.ConfirmWindow(ConfirmType.reset_progress);
    }

    public void ResetScores()
    {
        menu_hud.ConfirmWindow(ConfirmType.reset_scores);
    }

    public void ShowScores()
    {
        menu_hud.ToSelectMenu(MenuType.scores);
    }
}
*/