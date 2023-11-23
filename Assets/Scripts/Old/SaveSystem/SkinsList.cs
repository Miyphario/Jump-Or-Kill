using System.Collections.Generic;
using UnityEngine;
using System;

/*
public class SkinsList : MonoBehaviour
{
    public List<GameObject> skin_obj = new List<GameObject>();
    public List<Skin> skins = new List<Skin>();

    public void SaveSkins()
    {
        SaveSystem.SaveSkins(skins);
    }

    public void LoadBuyedSkins()
    {
        List<Skin> t = SaveSystem.LoadSkins();
        if (t != null)
        {
            for (int i = 0;i < t.Count;i++)
            {
                skins[i].buyed = t[i].buyed;
                skins[i].selected = t[i].selected;
            }
        }
    }

    public GameObject LoadSkinByName(string skin_name)
    {
        if (skins != null && skin_obj != null)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (skin_name == skins[i].skin_name)
                {
                    return skin_obj[i];
                }
            }
        }
        else
        {
            return null;
        }

        return null;
    }

    public GameObject GetSelectedSkin()
    {
        if (skins != null && skin_obj != null)
        {
            foreach (Skin s in skins)
            {
                if (s.selected)
                {
                    return LoadSkinByName(s.skin_name);
                }
            }
        }
        else
        {
            return null;
        }

        return null;
    }

    public void UnlockSkin(string skin_name)
    {
        int snum = -1;

        if (skins != null && skin_obj != null)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (skin_name == skins[i].skin_name)
                {
                    snum = i;
                    break;
                }
            }
        }

        skins[snum].buyed = true;
        SaveSkins();
    }

    public void UnbuySkin(string skin_name)
    {
        int snum = -1;

        if (skins != null && skin_obj != null)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                if (skin_name == skins[i].skin_name)
                {
                    snum = i;
                    break;
                }
            }
        }

        skins[snum].buyed = false;
        SaveSkins();
    }
}
*/