using UnityEngine;
using UnityEngine.UI;

/*
public class BoostPanel: MonoBehaviour
{
    [HideInInspector]
    public BoostData my_boost;
    [HideInInspector]
    public Sprite my_boost_sprite;
    [HideInInspector]
    public int my_index;

    public Text money_text;
    public Text boost_name_text;
    public Image price_type_image;
    public GameObject buy_button;
    public Image boost_image;

    [HideInInspector]
    public MenuHUD menu_hud;

    [HideInInspector]
    public bool loaded = false;
    [HideInInspector]
    public BoostsPanel parent_panel;

    public void SetSoundsClick(MenuSounds sound_manager)
    {
        buy_button.transform.GetComponent<Button>().onClick.AddListener(sound_manager.PlaySelect);
    }

    public void UpdateBoost()
    {
        boost_name_text.text = my_boost.render_name;
        switch (my_boost.price_type)
        {
            case PriceType.money:
                price_type_image.sprite = menu_hud.sprite_money;
                break;

            case PriceType.gems:
                price_type_image.sprite = menu_hud.sprite_gem;
                break;

            case PriceType.ads:
                price_type_image.sprite = menu_hud.sprite_ads;
                break;

            case PriceType.snowballs:
                price_type_image.sprite = menu_hud.sprite_snowball;
                break;
        }
    }

    public void UpdatePanel()
    {
        if (my_boost.buyed)
        {
            money_text.text = "Buyed";

            if (buy_button.activeSelf == true)
                buy_button.SetActive(false);
        }
        else
        {
            money_text.text = "" + my_boost.price;

            if (buy_button.activeSelf == false)
                buy_button.SetActive(true);

        }
    }

    public void BuyBoost()
    {
        if (loaded)
        {
            BoostsList bs = menu_hud.GetBoosts();
            GameData data = SaveSystem.LoadData();

            if (data != null && bs.boosts != null)
            {
                if (!bs.boosts[my_index].buyed)
                {
                    bool buyAccess = false;
                    switch (bs.boosts[my_index].price_type)
                    {
                        case PriceType.money:
                            if (data.money >= bs.boosts[my_index].price)
                            {
                                data.money -= bs.boosts[my_index].price;

                                buyAccess = true;
                            }
                            break;

                        case PriceType.gems:
                            if (data.gems >= bs.boosts[my_index].price)
                            {
                                data.gems -= bs.boosts[my_index].price;

                                buyAccess = true;
                            }
                            break;

                        case PriceType.ads:

                            break;

                        case PriceType.snowballs:
                            if (data.snowballs >= bs.boosts[my_index].price)
                            {
                                data.snowballs -= bs.boosts[my_index].price;

                                buyAccess = true;
                            }
                            break;
                    }

                    if (buyAccess)
                    {
                        bs.boosts[my_index].buyed = true;

                        SaveSystem.SaveData(data);
                        bs.SaveBoosts();
                    }

                    menu_hud.UpdateMoneyPanels();

                    if (parent_panel != null)
                        parent_panel.UpdateBoosts();
                }
            }

            menu_hud.DestroyBoostsList();
        }
    }
}
*/