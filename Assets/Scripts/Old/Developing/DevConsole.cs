using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
public class DevConsole : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rect_transfrom;
    [SerializeField] private Canvas canvas;
    public Button ok_button;
    public InputField input_field;
    public MenuHUD menu_hud;
    public Text history;

    //Commands help
    public Text commands_text;
    private List<string> commands = new List<string>();

    private void Start()
    {
        ok_button.onClick.AddListener(OkButton);
        input_field.onEndEdit.AddListener(delegate { OkButton(); });
        history.text = "";

        commands.Add("money");
        commands.Add("gem");
        commands.Add("snowball");
        commands.Add("booster");
    }

    private void Update()
    {
#if UNITY_EDITOR
        string input = input_field.text;
        input = input.Replace(" ", "");
        if (input != "")
        {
            foreach (string s in commands)
            {

            }
        }
#endif
    }

    private void OnEnable()
    {
        RectTransform cT = canvas.gameObject.transform.GetComponent<RectTransform>();
        if (rect_transfrom.anchoredPosition.x < 0 - rect_transfrom.rect.width / 2 || rect_transfrom.anchoredPosition.y < 0 - rect_transfrom.rect.height / 2 || rect_transfrom.anchoredPosition.x > cT.rect.width - rect_transfrom.rect.width / 2 || rect_transfrom.anchoredPosition.y > cT.rect.height - rect_transfrom.rect.height / 2)
        {
            rect_transfrom.anchoredPosition = new Vector2(400, 280);
        }
    }

    private void OkButton()
    {
        if (input_field.text.Length > 0)
        {
            string[] it = input_field.text.Split(' ');

            switch (it[0])
            {
                case "money":
                    if (it.Length > 2 && it[1] != "" && it[2] != "")
                    {
                        GameData d = SaveSystem.LoadData();
                        int m;
                        int.TryParse(it[2], out m);
                        if (it[1] == "+")
                        {
                            d.money += m;
                        }
                        else if (it[1] == "-")
                        {
                            d.money -= m;
                        }
                        SaveSystem.SaveData(d);
                    }
                    break;

                case "gem":
                    if (it.Length > 2 && it[1] != "" && it[2] != "")
                    {
                        GameData d = SaveSystem.LoadData();
                        int m;
                        int.TryParse(it[2], out m);
                        if (it[1] == "+")
                        {
                            d.gems += m;
                        }
                        else if (it[1] == "-")
                        {
                            d.gems -= m;
                        }
                        SaveSystem.SaveData(d);
                    }
                    break;

                case "snowball":
                    if (it.Length > 2 && it[1] != "" && it[2] != "")
                    {
                        GameData d = SaveSystem.LoadData();
                        int m;
                        int.TryParse(it[2], out m);
                        if (it[1] == "+")
                        {
                            d.snowballs += m;
                        }
                        else if (it[1] == "-")
                        {
                            d.snowballs -= m;
                        }
                        SaveSystem.SaveData(d);
                    }
                    break;

                case "booster":
                    if (it.Length > 2 && it[1] != "" && it[2] != "")
                    {
                        BoostsList d = menu_hud.GetBoosts();

                        switch (it[2])
                        {
                            case "speed":
                                if (it[1] == "+")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.speed);
                                    d.boosts[q].buyed = true;
                                }
                                else if (it[1] == "-")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.speed);
                                    d.boosts[q].buyed = false;
                                }
                                break;

                            case "defuse_kit":
                                if (it[1] == "+")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.defuse_kit);
                                    d.boosts[q].buyed = true;
                                }
                                else if (it[1] == "-")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.defuse_kit);
                                    d.boosts[q].buyed = false;
                                }
                                break;

                            case "cloud":
                                if (it[1] == "+")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.cloud);
                                    d.boosts[q].buyed = true;
                                }
                                else if (it[1] == "-")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.cloud);
                                    d.boosts[q].buyed = false;
                                }
                                break;

                            case "first_aid_kit":
                                if (it[1] == "+")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.health);
                                    d.boosts[q].buyed = true;
                                }
                                else if (it[1] == "-")
                                {
                                    int q = d.LoadBoostByEnum(BoostType.health);
                                    d.boosts[q].buyed = false;
                                }
                                break;
                        }

                        d.SaveBoosts();
                        menu_hud.DestroyBoostsList();
                    }
                    break;

                case "skin":
                    if (it.Length > 2 && it[1] != null && it[2] != null)
                    {
                        SkinsList s = menu_hud.GetSkins();
                        GameObject gos = s.LoadSkinByName(it[2]);

                        switch (it[1])
                        {
                            case "unlock":
                                if (gos != null)
                                {
                                    s.UnlockSkin(it[2]);
                                }

                                menu_hud.DestroySkinsList();
                                break;

                            case "sell":
                                if (gos != null)
                                {
                                    s.UnbuySkin(it[2]);
                                }

                                menu_hud.DestroySkinsList();
                                break;
                        }
                    }
                    break;
            }

            if (menu_hud != null)
            {
                menu_hud.UpdateMoneyPanels();
            }

            if (history.text.Length > 0)
            {
                history.text = history.text + "\n" + input_field.text;
            }
            else
            {
                history.text = input_field.text;
            }

            input_field.text = "";
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        RectTransform mht = menu_hud.gameObject.transform.GetComponent<RectTransform>();
        rect_transfrom.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    
}
*/