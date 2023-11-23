using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
public enum MenuType
{
    main,
    settings,
    shop,
    scores
}

public class MenuHUD : MonoBehaviour
{
    public MoneyPanel money_panel;

    public GameObject main_menu;
    public GameObject shop_menu;
    public GameObject settings_menu;
    public GameObject scores_menu;
    public GameObject enemy;

    public GameObject test_skin;
    public GameObject safe_area;

    [HideInInspector]
    public GameObject skin_to_destroy;
    public GameObject skins_list_obj;

    public GameObject boosts_list_obj;

    public GameObject confirm_window;

    public CreateScoresInfo scores_to_update;

    public GameObject debug_panel;

    public MenuSounds sound_manager;

    private GameObject skins_list_to_destroy;
    private GameObject boosts_list_to_destroy;

    [Header("Price sprites")]
    public Sprite sprite_gem;
    public Sprite sprite_money;
    public Sprite sprite_ads;
    public Sprite sprite_snowball;

    [HideInInspector]
    public MenuType selected_menu;

    [Header("Ads")]
    public AdsController ads_controller;

    private void Start()
    {
        UpdateMoneyPanels();

        SkinsList sk = GetSkins();
        if (sk != null)
        {
            if (enemy != null)
            {
                MenuEnemy me = enemy.transform.GetComponent<MenuEnemy>();
                me.ChangeSkin(sk.GetSelectedSkin());
                DestroySkinsList();
            }
        }
    }

    public void ToSelectMenu(MenuType type)
    {
        DestroySkinsList();

        if (skin_to_destroy != null)
            Destroy(skin_to_destroy);

        if (enemy.activeSelf)
            enemy.SetActive(false);

        if (shop_menu.activeSelf)
        {
            shop_menu.SetActive(false);
        }

        if (settings_menu.activeSelf)
        {
            settings_menu.SetActive(false);
        }

        if (scores_menu.activeSelf)
        {
            scores_menu.SetActive(false);
        }

        if (main_menu.activeSelf)
        {
            main_menu.SetActive(false);
        }

        switch (type)
        {
            case MenuType.main:
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(true);

                    SkinsList sk = GetSkins();
                    if (sk != null)
                    {
                        if (enemy != null)
                        {
                            MenuEnemy me = enemy.transform.GetComponent<MenuEnemy>();
                            me.ChangeSkin(sk.GetSelectedSkin());
                            DestroySkinsList();
                        }
                    }
                }

                if (!main_menu.activeSelf)
                {
                    main_menu.SetActive(true);
                }

                money_panel.ChangePosition(false, main_menu);

                UpdateMoneyPanels();
                break;

            case MenuType.settings:
                if (!settings_menu.activeSelf)
                {
                    settings_menu.SetActive(true);
                }
                break;

            case MenuType.shop:
                if (!shop_menu.activeSelf)
                {
                    shop_menu.SetActive(true);
                }

                money_panel.ChangePosition(true, shop_menu);

                UpdateMoneyPanels();
                break;

            case MenuType.scores:
                if (!scores_menu.activeSelf)
                {
                    scores_menu.SetActive(true);
                }

                scores_to_update.UpdateScores();
                break;
        }

        selected_menu = type;
    }

    public void ConfirmWindow(ConfirmType type)
    {
        if (!confirm_window.activeSelf)
            confirm_window.SetActive(true);

        if (safe_area.activeSelf)
            safe_area.SetActive(false);

        if (enemy.activeSelf)
            enemy.SetActive(false);

        ConfirmWindow wind = confirm_window.transform.GetComponent<ConfirmWindow>();
        switch (type)
        {
            case ConfirmType.reset_progress:
                wind.ShowWindow("Are you sure you want to reset all progress?", type);
                break;

            case ConfirmType.reset_scores:
                wind.ShowWindow("Are you sure you want to reset all records?", type);
                break;

            case ConfirmType.quit:
                wind.ShowWindow("Are you sure you want to quit the game?", type);
                break;
        }
    }

    public string UpdatePriceText(PriceType price_type)
    {
        GameData data = SaveSystem.LoadData();
        string txt = "0";
        if (data != null)
        {
            switch (price_type)
            {
                case PriceType.gems:
                    txt = "" + data.gems;
                    break;

                case PriceType.money:
                    txt = "" + data.money;
                    break;

                case PriceType.snowballs:
                    txt = "" + data.snowballs;
                    break;
            }
        }

        return txt;
    }

    public void UpdateMoneyPanels()
    {
        money_panel.UpdatePanel();
    }

    public SkinsList GetSkins()
    {
        if (skins_list_to_destroy != null)
        {
            SkinsList sl = skins_list_to_destroy.transform.GetComponent<SkinsList>();
            sl.LoadBuyedSkins();

            return sl;
        }
        else
        {
            skins_list_to_destroy = Instantiate(skins_list_obj, new Vector3(0, 0, 0), Quaternion.identity);
            SkinsList sl = skins_list_to_destroy.transform.GetComponent<SkinsList>();
            sl.LoadBuyedSkins();

            return sl;
        }
    }

    public BoostsList GetBoosts()
    {
        if (boosts_list_to_destroy != null)
        {
            BoostsList sl = boosts_list_to_destroy.transform.GetComponent<BoostsList>();
            sl.LoadBuyedBoosts();

            return sl;
        }
        else
        {
            boosts_list_to_destroy = Instantiate(boosts_list_obj, new Vector3(0, 0, 0), Quaternion.identity);
            BoostsList sl = boosts_list_to_destroy.transform.GetComponent<BoostsList>();
            sl.LoadBuyedBoosts();

            return sl;
        }
    }

    public void DestroySkinsList()
    {
        if (skins_list_to_destroy != null)
            Destroy(skins_list_to_destroy);
    }

    public void DestroyBoostsList()
    {
        if (boosts_list_to_destroy != null)
            Destroy(boosts_list_to_destroy);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selected_menu != MenuType.main)
            {
                ToSelectMenu(MenuType.main);
            }
            else
            {
                ConfirmWindow(ConfirmType.quit);
            }
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            debug_panel.SetActive(!debug_panel.activeSelf);
        }
#endif
    }
}
*/