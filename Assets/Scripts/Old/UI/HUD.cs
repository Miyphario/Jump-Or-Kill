using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    /*
    public Text render_text;

    private int frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private float updateRate = 4.0f;

    public GameObject player;
    private BaseEntity player_entity;

    public GameObject gameover_overlay;
    public Image gameover_bo_image;
    private float gameover_alpha = 0;

    public GameObject[] buttons_to_destroy;
    public GameObject[] buttons_to_deactivate;
    public GameObject button_pause;
    public GameObject pause_menu;
    public GameObject safe_area;
    public GameObject confirm_window;

    public Text gameover_text;

    public Text score_text;
    public GameObject money_panel;

    public GameController gamecontroller;

    private Coroutine mp_coroutine;

    public GameSounds sounds_manager;

    private bool take_screenshot = false;
    private float time_to_screenshot = -1f;

    [Header("ADS")]
    public AdsController ads_controller;
    public Button ads_button;
    public GameObject ads_panel;
    private int ads_health = 2;

    private void Start()
    {
        player_entity = player.transform.GetComponent<BaseEntity>();

        if (!GameController.IsMobile())
        {
            foreach (GameObject b in buttons_to_destroy)
            {
                if (b != null)
                    Destroy(b);
            }

            Destroy(button_pause);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        score_text.text = "" + Mathf.RoundToInt(player_entity.walk_distance);

        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0f / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }

        render_text.text = "fps: " + Mathf.RoundToInt(fps);

        if (take_screenshot)
        {
            if (time_to_screenshot > 0f)
            {
                time_to_screenshot -= Time.unscaledDeltaTime;
            }
            else
            {
                time_to_screenshot = -1f;
                pause_menu.SetActive(true);
                take_screenshot = false;
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine("AddToAlpha");

        if (safe_area.activeSelf)
            safe_area.SetActive(false);

        if (buttons_to_destroy != null)
        {
            foreach (GameObject n in buttons_to_destroy)
            {
                if (n != null)
                    Destroy(n);
            }
        }

        if (gamecontroller != null)
            gamecontroller.SaveScore();

        if (gameover_text != null)
        {
            int dist = Mathf.RoundToInt(player_entity.walk_distance);
            Scores sc = SaveSystem.LoadBestScore();
            GameData tg = SaveSystem.LoadData();

            if (sc != null)
            {
                if (sc.distance_traveled > 0)
                {
                    gameover_text.text = "You lose!" + "\n\nDistance traveled: " + dist + " | Best: " + sc.distance_traveled + "\nKills: " + player_entity.kills + " | Best: " + sc.kills + "\nTotal money: " + tg.money;
                }
                else
                {
                    gameover_text.text = "You lose!" + "\n\nDistance traveled: " + dist + "\nKills: " + player_entity.kills + "\nTotal money: " + tg.money;

                }
            }
            else
            {
                gameover_text.text = "You lose!" + "\n\nDistance traveled: " + dist + "\nKills: " + player_entity.kills + "\nTotal money: " + tg.money;
            }

            ManagerScript.AddLeaderboards(dist);

            if (dist >= 400)
            {
                ManagerScript.UnlockAchievement(GPGSIds.achievement_the_first_steps);
            }

            if (dist >= 800)
            {
                ManagerScript.UnlockAchievement(GPGSIds.achievement_twice_as_good);
            }

            if (dist >= 1200)
            {
                ManagerScript.UnlockAchievement(GPGSIds.achievement_how_far_will_you_go);
            }

            if (dist >= 2000)
            {
                ManagerScript.UnlockAchievement(GPGSIds.achievement_are_you_a_robot);
            }

            if (dist >= 5000)
            {
                ManagerScript.UnlockAchievement(GPGSIds.achievement_you_are_deity);

                SkinsList s = gamecontroller.GetSkins();
                s.UnlockSkin("TheGod");
                gamecontroller.DestroySkinsList();
            }
        }

        if (gameover_overlay != null)
            gameover_overlay.SetActive(true);
    }

    public void GameEnd()
    {
        sounds_manager.PlayBlop();
        sounds_manager.ActionWithSound(ActionSound.quit);

        foreach (GameObject bd in buttons_to_deactivate)
        {
            if (bd != null)
            {
                if (bd.activeSelf)
                    bd.SetActive(false);
            }
        }
    }

    public void GameRestart()
    {
        sounds_manager.PlayBlop();
        sounds_manager.ActionWithSound(ActionSound.restart);

        foreach (GameObject bd in buttons_to_deactivate)
        {
            if (bd != null)
            {
                if (bd.activeSelf)
                    bd.SetActive(false);
            }
        }
    }

    public void OpenMenu()
    {
        sounds_manager.PlayBlop();
        sounds_manager.ActionWithSound(ActionSound.to_menu);

        foreach (GameObject bd in buttons_to_deactivate)
        {
            if (bd != null)
            {
                if (bd.activeSelf)
                    bd.SetActive(false);
            }
        }
    }

    IEnumerator AddToAlpha()
    {
        while (gameover_alpha < 0.46f)
        {
            gameover_alpha += 0.04f;
            Color tc = gameover_bo_image.color;
            gameover_bo_image.color = new Color(tc.r, tc.g, tc.b, gameover_alpha);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ShowMoneyPanel(float time, bool show, Sprite money_sprite, PriceType money_type)
    {
        if (mp_coroutine != null)
            StopCoroutine(mp_coroutine);

        mp_coroutine = StartCoroutine(ShowMPanel(time, show, money_sprite, money_type));
    }

    public IEnumerator ShowMPanel(float time, bool show, Sprite money_sprite, PriceType money_type)
    {
        if (show)
        {
            RectTransform myT = transform.GetComponent<RectTransform>();
            Vector2 pInvp = Camera.main.WorldToViewportPoint(player.transform.position);;
            Vector2 pInScreen = new Vector2(pInvp.x * myT.rect.width, pInvp.y * myT.rect.height);

            pInScreen = new Vector2(Mathf.Clamp(pInScreen.x, 0f, myT.rect.width - money_panel.transform.GetComponent<RectTransform>().rect.width * 0.2f), Mathf.Clamp(pInScreen.y, 0f, myT.rect.height - money_panel.transform.GetComponent<RectTransform>().rect.height * 1.5f));

            if (!money_panel.activeSelf)
            {
                money_panel.SetActive(true);
            }

            money_panel.transform.Find("MoneyImage").transform.GetComponent<Image>().sprite = money_sprite;
            Text mt = money_panel.transform.Find("MoneyText").transform.GetComponent<Text>();

            switch (money_type)
            {
                case PriceType.money:
                    mt.text = "" + player_entity.money;
                    break;

                case PriceType.gems:
                    mt.text = "" + player_entity.gems;
                    break;

                case PriceType.snowballs:
                    mt.text = "" + player_entity.snowballs;
                    break;
            }

            money_panel.transform.GetComponent<RectTransform>().anchoredPosition = pInScreen;

            yield return new WaitForSeconds(time);

            mp_coroutine = StartCoroutine(ShowMPanel(0f, false, null, PriceType.money));
        }
        else
        {
            if (money_panel.activeSelf)
            {
                money_panel.SetActive(false);
            }
        }
    }

    public void TogglePause()
    {
        GameController.gameIsPaused = !GameController.gameIsPaused;

        if (GameController.gameIsPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            if (!pause_menu.activeSelf)
                pause_menu.SetActive(true);

            if (safe_area.activeSelf)
                safe_area.SetActive(false);

            if (GameController.IsMobile())
            {
                if (button_pause.activeSelf)
                    button_pause.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            if (pause_menu.activeSelf)
                pause_menu.SetActive(false);

            if (!safe_area.activeSelf)
                safe_area.SetActive(true);

            if (GameController.IsMobile())
            {
                if (!button_pause.activeSelf)
                    button_pause.SetActive(true);
            }
        }
    }

    public bool TogglePauseAds(bool pause, bool destroy_player)
    {
        if (pause)
        {
            if (safe_area.activeSelf)
                safe_area.SetActive(false);

            if (ads_health > 0)
            {
                if (ads_controller.AdsReady())
                {
                    GameController.gameIsPaused = true;

                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    if (!ads_panel.activeSelf)
                        ads_panel.SetActive(true);

                    return true;
                }
            }
        }
        else
        {
            if (!destroy_player)
            {
                GameController.gameIsPaused = false;
                Time.timeScale = 1;
                AudioListener.pause = false;
                if (ads_panel.activeSelf)
                    ads_panel.SetActive(false);

                if (!safe_area.activeSelf)
                    safe_area.SetActive(true);

                player_entity.AddEffect(new BoostData(BoostType.health, 0f, 3f, true), player.transform.position);
                ads_health--;
            }
            else
            {
                GameController.gameIsPaused = false;
                Time.timeScale = 1;
                AudioListener.pause = false;
                if (ads_panel.activeSelf)
                    ads_panel.SetActive(false);

                player_entity.DestroyMe(null, true);
            }
        }

        return false;
    }

    public void CloseAdsPanel()
    {
        GameController.gameIsPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        if (ads_panel.activeSelf)
            ads_panel.SetActive(false);

        player_entity.DestroyMe(null, true);
    }

    public void TakeScreenshot()
    {
        pause_menu.SetActive(false);
        GameController.TakeScreenshot();
        take_screenshot = true;
        time_to_screenshot = 0.1f;
    }

    public void ConfirmWindow(ConfirmType type)
    {
        if (!confirm_window.activeSelf)
            confirm_window.SetActive(true);

        GameObject panel = pause_menu.transform.GetChild(0).transform.gameObject;
        if (panel.activeSelf)
            panel.SetActive(false);

        ConfirmWindow wind = confirm_window.transform.GetComponent<ConfirmWindow>();
        switch (type)
        {
            case ConfirmType.in_menu:
                wind.ShowWindow("Are you sure you want to exit to the menu? All current progress will be lost.", type);
                break;

            case ConfirmType.quit:
                wind.ShowWindow("Are you sure you want to log out of the game? All current progress will be lost.", type);
                break;

            case ConfirmType.game_restart:
                wind.ShowWindow("Are you really restarting the game? All current progress will be lost.", type);
                break;
        }
    }

    public void ToMenu()
    {
        ConfirmWindow(ConfirmType.in_menu);
    }

    public void RestartGame()
    {
        ConfirmWindow(ConfirmType.game_restart);
    }

    public void ExitGame()
    {
        ConfirmWindow(ConfirmType.quit);
    }

    public void ShowRewardAds()
    {
        ads_controller.ShowAds();
    }

    public void ActivateAdsButton()
    {
        ads_button.interactable = true;
    }
    */
}
