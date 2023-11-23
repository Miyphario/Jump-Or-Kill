using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
public class MoneyPanel : MonoBehaviour
{
    public Text money_text, gems_text, other_text;
    public Image money_image, gems_image, other_image;

    public MenuHUD menu_hud;

    private RectTransform rect_transform;

    private void Awake()
    {
        rect_transform = transform.GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        money_text.text = menu_hud.UpdatePriceText(PriceType.money);
        gems_text.text = menu_hud.UpdatePriceText(PriceType.gems);

        if (ManagerScript.GetCurrentSeason() == EventType.christmass)
        {
            other_image.gameObject.SetActive(true);
            other_text.gameObject.SetActive(true);

            other_image.sprite = menu_hud.sprite_snowball;
            other_text.text = menu_hud.UpdatePriceText(PriceType.snowballs);
        }
        else
        {
            other_image.gameObject.SetActive(false);
            other_text.gameObject.SetActive(false);
        }
    }

    public void ChangePosition(bool shop, GameObject parent)
    {
        if (transform.parent != parent.transform)
        {
            transform.SetParent(parent.transform);

            Vector3 tpos = rect_transform.anchoredPosition3D;
            if (!shop)
            {
                rect_transform.anchorMin = new Vector2(0, 1);
                rect_transform.anchorMax = new Vector2(1, 1);
                rect_transform.anchoredPosition3D = new Vector3(tpos.x, -31, tpos.z);
            }
            else
            {
                rect_transform.anchorMin = new Vector2(0, 0);
                rect_transform.anchorMax = new Vector2(1, 0);
                rect_transform.anchoredPosition3D = new Vector3(tpos.x, 31, tpos.z);
            }
        }
    }
}
*/