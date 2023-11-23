using System;
using System.Collections.Generic;
using UnityEngine;

/*
public class SkinsPanel : MonoBehaviour
{
    public SkinCategory current_skin_category = SkinCategory.all;

    public List<GameObject> skin_panels = new List<GameObject>();
    public GameObject skin_panel_prefab;
    
    public MenuHUD menu_hud;

    private void OnEnable()
    {
        UpdateSkins(true, SkinCategory.all);
    }

    public void UpdateSkins(bool forceUpdate, SkinCategory category)
    {
        if (!forceUpdate)
        {
            if (current_skin_category != category)
            {
                current_skin_category = category;

                SkinsList sk = menu_hud.GetSkins();

                if (skin_panels != null)
                {
                    foreach (GameObject g in skin_panels)
                    {
                        if (g != null)
                            Destroy(g);
                    }

                    skin_panels.Clear();
                }
                else
                {
                    skin_panels = new List<GameObject>();
                }

                Transform content = transform.Find("Content");
                if (sk.skins != null)
                {
                    DateTime curTime = DateTime.Now;
                    for (int i = 0; i < sk.skins.Count; i++)
                    {
                        EventType et = sk.skins[i].myEvent;
                        if (ManagerScript.AccessEvent(et))
                        {
                            if (current_skin_category == SkinCategory.all)
                            {
                                CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                            }
                            else if (current_skin_category == SkinCategory.buyed)
                            {
                                if (sk.skins[i].buyed)
                                    CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                            }
                            else if (current_skin_category == SkinCategory.noPrice)
                            {
                                if (sk.skins[i].category == SkinCategory.noPrice)
                                    CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                            }
                            else
                            {
                                if (sk.skins[i].category == current_skin_category)
                                {
                                    CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            SkinsList sk = menu_hud.GetSkins();

            if (skin_panels != null)
            {
                foreach (GameObject g in skin_panels)
                {
                    if (g != null)
                        Destroy(g);
                }

                skin_panels.Clear();
            }
            else
            {
                skin_panels = new List<GameObject>();
            }

            Transform content = transform.Find("Content");
            if (sk.skins != null)
            {
                DateTime curTime = DateTime.Now;
                for (int i = 0; i < sk.skins.Count; i++)
                {
                    EventType et = sk.skins[i].myEvent;
                    if (ManagerScript.AccessEvent(et))
                    {
                        if (current_skin_category == SkinCategory.all)
                        {
                            CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                        }
                        else if (current_skin_category == SkinCategory.buyed)
                        {
                            if (sk.skins[i].buyed)
                                CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                        }
                        else if (current_skin_category == SkinCategory.noPrice)
                        {
                            if (sk.skins[i].category == SkinCategory.noPrice)
                                CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                        }
                        else
                        {
                            if (sk.skins[i].category == current_skin_category)
                            {
                                CreateSkinPanel(sk.skins[i], sk.LoadSkinByName(sk.skins[i].skin_name), i);
                            }
                        }
                    }
                }
            }
        }
        menu_hud.DestroySkinsList();
    }

    public void UpdatePanels()
    {
        SkinsList sk = menu_hud.GetSkins();
        if (skin_panels != null)
        {
            for (int i = 0; i < sk.skins.Count; i++)
            {
                foreach (GameObject g in skin_panels)
                {
                    SkinPanel s = g.transform.GetComponent<SkinPanel>();
                    if (s.my_index == i)
                    {
                        s.loaded = true;
                        s.my_skin = sk.skins[i];
                        s.UpdatePanel();
                    }
                }
            }
        }

        menu_hud.DestroySkinsList();
    }

    public void CreateSkinPanel(Skin skin, GameObject skin_obj, int index)
    {
        Transform content = transform.Find("Content");

        GameObject p = Instantiate(skin_panel_prefab, new Vector3(0, 0, 0), Quaternion.identity, content);
        skin_panels.Add(p);
        SkinPanel sp = p.transform.GetComponent<SkinPanel>();

        sp.my_index = index;
        sp.menu_hud = menu_hud;
        sp.parent_panel = transform.GetComponent<SkinsPanel>();
        sp.my_skin = skin;
        sp.my_skin_obj = skin_obj;
        sp.SetSoundSelect(menu_hud.sound_manager);
        sp.UpdatePanel();
        sp.UpdateSkin();
        sp.loaded = true;
    }
}
*/