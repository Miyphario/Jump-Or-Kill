using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    /*
    public GameObject effect_to_create;
    public List<Effect> effects = new List<Effect>();
    public GameObject heart_prefab;

    [HideInInspector]
    public GameObject hearts_panel;

    [HideInInspector]
    public List<GameObject> hearts = new List<GameObject>();

    [HideInInspector]
    public BaseEntity my_entity;

    public GameObject hud;
    private RectTransform safe_area_transform;
    private Transform effect_frames;

    private void Awake()
    {
        hud = GameObject.Find("HUD");
        safe_area_transform = hud.transform.GetChild(0).transform.GetComponent<RectTransform>();
        effect_frames = hud.transform.GetChild(0).transform.Find("EffectFrames").transform;
    }

    private void Start()
    {
        Invoke("ClearNullEffects", 10f);

        UpdateHearts(true, new Vector2(0, 0));
    }

    public bool CreateEffect(BoostData effect, Vector2 boost_position)
    {
        bool add = true;

        if (effects != null)
        {
            if (effect.boost_type != BoostType.health)
            {
                foreach (Effect ef in effects)
                {
                    if (ef != null)
                    {
                        if (ef.effect.boost_type == effect.boost_type)
                        {
                            add = false;
                        }
                    }
                }
            }

            if (add)
            {
                if (effect.boost_type == BoostType.health)
                {
                    if (my_entity.health < my_entity.max_health)
                    {
                        InitEffect(effect, boost_position);
                    }
                    else
                    {
                        add = false;
                    }
                }
                else
                {
                    InitEffect(effect, boost_position);
                }
            }
        }
        else
        {
            effects = new List<Effect>();

            if (effect.boost_type == BoostType.health)
            {
                if (my_entity.health < my_entity.max_health)
                {
                    InitEffect(effect, boost_position);
                }
                else
                {
                    add = false;
                }
            }
            else
            {
                InitEffect(effect, boost_position);
            }
        }

        return add;
    }

    public void RemoveEffect(Effect effect)
    {
        effects.Remove(effect);
    }

    public Effect GetEffectByEnum(BoostType effect_type)
    {
        if (effects != null)
        {
            foreach (Effect e in effects)
            {
                if (e.effect.boost_type == effect_type)
                {
                    return e;
                }
            }
        }

        return null;
    }    

    private void InitEffect(BoostData effect, Vector2 boost_pos)
    {
        GameObject s = Instantiate(effect_to_create, transform);
        Effect e = s.GetComponent<Effect>();

        e.frame_parent = effect_frames;
        e.effect = effect;
        e.manager = transform.GetComponent<EffectsManager>();
        e.player = my_entity;
        e.my_boost_pos = boost_pos;
        e.SetEffect();
        effects.Add(e);
    }

    private void ClearNullEffects()
    {
        if (effects != null)
        {
            foreach (Effect e in effects)
            {
                if (e == null)
                    effects.Remove(e);
            }
        }

        Invoke("ClearNullEffects", 10f);
    }

    public void UpdateHearts(bool started, Vector2 boost_pos)
    {
        if (hearts_panel != null)
        {
            if (hearts.Count < my_entity.health)
            {
                StartCoroutine(CreateHeart(0.2f, true, started, boost_pos));
            }
            else if (hearts.Count > my_entity.health)
            {
                StartCoroutine(CreateHeart(0.2f, false, started, boost_pos));
            }
        }
    }

    IEnumerator CreateHeart(float time, bool create, bool started, Vector2 boost_pos)
    {
        if (!create)
        {
            while (hearts.Count > my_entity.health)
            {
                Destroy(hearts[hearts.Count - 1]);
                hearts.RemoveAt(hearts.Count - 1);

                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (hearts.Count < my_entity.health)
            {
                GameObject h = Instantiate(heart_prefab, hud.transform.GetChild(0).transform, false);
                Heart ht = h.transform.GetComponent<Heart>();
                RectTransform finishT = hearts_panel.transform.GetComponent<RectTransform>();
                ht.safe_area_transform = safe_area_transform;
                ht.parent_transform = hearts_panel.transform;
                ht.finish_transform = finishT;
                ht.my_number = hearts.Count;

                if (started)
                {
                    h.GetComponent<RectTransform>().anchoredPosition = finishT.anchoredPosition;
                    ht.anim_played = true;
                    h.transform.SetParent(hearts_panel.transform, false);
                }
                else
                {
                    Vector2 rt = Camera.main.WorldToViewportPoint(boost_pos);
                    RectTransform hudT = hud.transform.GetComponent<RectTransform>();
                    Vector2 hPos = new Vector2(rt.x * hudT.rect.width - hudT.rect.width, rt.y * hudT.rect.height - hudT.rect.height);
                    h.GetComponent<RectTransform>().anchoredPosition = hPos;
                }
                
                hearts.Add(h);

                yield return new WaitForSeconds(0.15f);
            }
        }
    }
    */
}
