using System.Collections.Generic;
using UnityEngine;
using System;

/*
public class BoostsList : MonoBehaviour
{
    public List<Sprite> boost_sprites = new List<Sprite>();
    public List<BoostData> boosts = new List<BoostData>();

    public void SaveBoosts()
    {
        SaveSystem.SaveBoosts(boosts);
    }

    public void LoadBuyedBoosts()
    {
        List<BoostData> t = SaveSystem.LoadBoosts();
        if (t != null)
        {
            for (int i = 0; i < t.Count; i++)
            {
                boosts[i].buyed = t[i].buyed;
            }
        }
    }

    public Sprite LoadBoostByName(string boost_name)
    {
        if (boosts != null && boost_sprites != null)
        {
            for (int i = 0; i < boosts.Count; i++)
            {
                if (boost_name == boosts[i].boost_name)
                {
                    return boost_sprites[i];
                }
            }
        }

        return null;
    }

    public int LoadBoostByEnum(BoostType boost_type)
    {
        if (boosts != null)
        {
            for (int i = 0; i < boosts.Count; i++)
            {
                if (boosts[i].boost_type == boost_type)
                {
                    return i;
                }
            }
        }

        return -1;
    }
}
*/