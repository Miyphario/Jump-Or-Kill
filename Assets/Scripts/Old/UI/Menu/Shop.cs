using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject main_shop, skins_shop, boosts_shop, weapons_shop;

    public void ToMainShop()
    {
        if (skins_shop.activeSelf)
            skins_shop.SetActive(false);

        if (boosts_shop.activeSelf)
            boosts_shop.SetActive(false);

        if (weapons_shop.activeSelf)
            weapons_shop.SetActive(false);

        if (!main_shop.activeSelf)
            main_shop.SetActive(true);
    }

    public void ToSkinsShop()
    {
        if (main_shop.activeSelf)
            main_shop.SetActive(false);

        if (boosts_shop.activeSelf)
            boosts_shop.SetActive(false);

        if (weapons_shop.activeSelf)
            weapons_shop.SetActive(false);

        if (!skins_shop.activeSelf)
            skins_shop.SetActive(true);
    }

    public void ToBoostsShop()
    {
        if (main_shop.activeSelf)
            main_shop.SetActive(false);

        if (skins_shop.activeSelf)
            skins_shop.SetActive(false);

        if (weapons_shop.activeSelf)
            weapons_shop.SetActive(false);

        if (!boosts_shop.activeSelf)
            boosts_shop.SetActive(true);
    }

    public void ToWeaponsShop()
    {
        if (main_shop.activeSelf)
            main_shop.SetActive(false);

        if (boosts_shop.activeSelf)
            boosts_shop.SetActive(false);

        if (skins_shop.activeSelf)
            skins_shop.SetActive(false);

        if (!weapons_shop.activeSelf)
            weapons_shop.SetActive(true);
    }
}
