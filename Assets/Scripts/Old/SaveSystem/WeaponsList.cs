using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class WeaponsList : MonoBehaviour
{
    public List<GameObject> weapon_obj = new List<GameObject>();
    public List<Weapon> weapons = new List<Weapon>();

    public void SaveWeapons()
    {
        SaveSystem.SaveWeapons(weapons);
    }

    public void LoadBuyedWeapons()
    {
        List<Weapon> t = SaveSystem.LoadWeapons();
        if (t != null)
        {
            for (int i = 0; i < t.Count; i++)
            {
                weapons[i].buyed = t[i].buyed;
                weapons[i].selected = t[i].selected;
            }
        }
    }

    public GameObject LoadWeaponByName(string skin_name)
    {
        if (weapons != null && weapon_obj != null)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (skin_name == weapons[i].weapon_name)
                {
                    return weapon_obj[i];
                }
            }
        }
        else
        {
            return null;
        }

        return null;
    }

    public GameObject GetSelectedWeapon()
    {
        if (weapons != null && weapon_obj != null)
        {
            foreach (Weapon s in weapons)
            {
                if (s.selected)
                {
                    return LoadWeaponByName(s.weapon_name);
                }
            }
        }
        else
        {
            return null;
        }

        return null;
    }
}
*/