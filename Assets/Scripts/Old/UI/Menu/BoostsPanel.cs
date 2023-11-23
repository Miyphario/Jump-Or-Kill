using System;
using System.Collections.Generic;
using UnityEngine;

/*
public class BoostsPanel : MonoBehaviour
{
    public List<GameObject> boosts_panels = new List<GameObject>();

    public GameObject boost_panel_prefab;

    public MenuHUD menu_hud;

    private void Start()
    {
        UpdateBoosts();
    }

    private void OnEnable()
    {
        UpdateBoosts();
    }

    public void UpdateBoosts()
    {
        BoostsList bs = menu_hud.GetBoosts();

        if (boosts_panels != null)
        {
            foreach (GameObject g in boosts_panels)
            {
                if (g != null)
                    Destroy(g);
            }

            boosts_panels.Clear();
        }
        else
        {
            boosts_panels = new List<GameObject>();
        }

        Transform content = transform.Find("Content");
        if (bs.boosts != null)
        {
            DateTime curTime = DateTime.Now;
            for (int i = 0; i < bs.boosts.Count; i++)
            {
                CreateBoostPanel(bs.boosts[i], bs.LoadBoostByName(bs.boosts[i].boost_name), i);
            }
        }

        menu_hud.DestroyBoostsList();
    }

    public void CreateBoostPanel(BoostData boost, Sprite boost_sprite, int index)
    {
        Transform content = transform.Find("Content");

        GameObject p = Instantiate(boost_panel_prefab, new Vector3(0, 0, 0), Quaternion.identity, content);
        boosts_panels.Add(p);
        BoostPanel sp = p.transform.GetComponent<BoostPanel>();

        sp.my_index = index;
        sp.menu_hud = menu_hud;
        sp.parent_panel = transform.GetComponent<BoostsPanel>();
        sp.my_boost = boost;
        sp.my_boost_sprite = boost_sprite;
        sp.boost_image.sprite = boost_sprite;
        sp.boost_image.SetNativeSize();
        sp.boost_image.rectTransform.sizeDelta = sp.boost_image.rectTransform.sizeDelta / 1.5f;
        sp.SetSoundsClick(menu_hud.sound_manager);
        sp.UpdatePanel();
        sp.UpdateBoost();
        sp.loaded = true;
    }
}
*/