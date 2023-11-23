using UnityEngine;
using UnityEngine.UI;

/*
public class SkinPanel : MonoBehaviour
{
    [HideInInspector]
    public Skin my_skin;
    [HideInInspector]
    public GameObject my_skin_obj;
    [HideInInspector]
    public int my_index;

    public Text money_text;
    public Text skin_name_text;
    public Image price_type_image;
    public GameObject buy_button;
    public GameObject select_button;
    public Text button_buy_text;

    public MenuHUD menu_hud;
    public GameObject my_enemy;

    [HideInInspector]
    public bool loaded = false;
    [HideInInspector]
    public SkinsPanel parent_panel;

    public void SetSoundSelect(MenuSounds sound_manager)
    {
        buy_button.transform.GetComponent<Button>().onClick.AddListener(sound_manager.PlaySelect);
        select_button.transform.GetComponent<Button>().onClick.AddListener(sound_manager.PlaySelect);
    }

    private void Awake()
    {
        //my_enemy = transform.Find("Menu_Enemy").gameObject;
    }

    public void UpdateSkin()
    {
        MenuEnemy e = my_enemy.transform.GetComponent<MenuEnemy>();
        e.transform.GetComponent<MenuEnemy>().ChangeSkin(my_skin_obj);
        skin_name_text.text = my_skin.render_name;
        switch (my_skin.price_type)
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

            case PriceType.noPrice:
                price_type_image.sprite = null;
                price_type_image.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdatePanel()
    {
        if (my_enemy != null)
        {
            if (my_skin.buyed)
            {
                if (my_skin.price_type != PriceType.noPrice)
                {
                    money_text.text = "Buyed";
                }
                else
                {
                    money_text.text = "Unlocked";
                }

                if (buy_button.activeSelf == true)
                    buy_button.SetActive(false);

                if (my_skin.selected)
                {
                    if (select_button.GetComponent<Button>().interactable == true)
                        select_button.GetComponent<Button>().interactable = false;

                    select_button.transform.GetChild(0).transform.GetComponent<Text>().text = "Selected";
                }
                else
                {
                    if (select_button.GetComponent<Button>().interactable == false)
                        select_button.GetComponent<Button>().interactable = true;
                    select_button.transform.GetChild(0).transform.GetComponent<Text>().text = "Select";
                }
            }
            else
            {
                if (my_skin.price_type != PriceType.noPrice)
                {
                    money_text.text = "" + my_skin.price;

                    button_buy_text.text = "Buy";
                }
                else
                {
                    money_text.text = my_skin.noPriceDesc;

                    button_buy_text.text = "Locked";

                    if (buy_button.GetComponent<Button>().interactable == true)
                    {
                        buy_button.GetComponent<Button>().interactable = false;
                    }
                }

                if (buy_button.activeSelf == false)
                    buy_button.SetActive(true);

                select_button.transform.GetChild(0).transform.GetComponent<Text>().text = "Select";

                if (select_button.GetComponent<Button>().interactable == true)
                    select_button.GetComponent<Button>().interactable = false;

            }
        }
    }

    public void BuySkin()
    {
        if (loaded)
        {
            SkinsList sk = menu_hud.GetSkins();
            GameData data = SaveSystem.LoadData();

            if (data != null && sk.skins != null)
            {
                if (!sk.skins[my_index].buyed)
                {
                    bool buyAccess = false;
                    switch (sk.skins[my_index].price_type)
                    {
                        case PriceType.money:
                            if (data.money >= sk.skins[my_index].price)
                            {
                                data.money -= sk.skins[my_index].price;

                                buyAccess = true;
                            }
                            break;

                        case PriceType.gems:
                            if (data.gems >= sk.skins[my_index].price)
                            {
                                data.gems -= sk.skins[my_index].price;

                                buyAccess = true;
                            }
                            break;

                        case PriceType.ads:

                            break;

                        case PriceType.snowballs:
                            if (data.snowballs >= sk.skins[my_index].price)
                            {
                                data.snowballs -= sk.skins[my_index].price;

                                buyAccess = true;
                            }
                            break;
                    }

                    if (buyAccess)
                    {
                        sk.skins[my_index].buyed = true;

                        SaveSystem.SaveData(data);
                        sk.SaveSkins();
                    }

                    menu_hud.UpdateMoneyPanels();

                    if (parent_panel != null)
                        parent_panel.UpdatePanels();
                }
            }

            menu_hud.DestroySkinsList();
        }
    }

    public void SelectSkin()
    {
        if (loaded)
        {
            SkinsList sk = menu_hud.GetSkins();

            foreach (Skin s in sk.skins)
            {
                s.selected = false;
            }

            sk.skins[my_index].selected = true;
            sk.SaveSkins();

            my_skin = sk.skins[my_index];

            if (parent_panel != null)
                parent_panel.UpdatePanels();

            menu_hud.DestroySkinsList();
        }
    }
}
*/