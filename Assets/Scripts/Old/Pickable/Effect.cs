using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    /*
    [HideInInspector]
    public BoostData effect;

    [HideInInspector]
    public EffectsManager manager;
    [HideInInspector]
    public Creature player;

    [HideInInspector]
    public GameObject my_frame;
    [HideInInspector]
    public Transform frame_parent;

    public GameObject frame_prefab;

    [HideInInspector]
    public Vector2 my_boost_pos;

    private void FixedUpdate()
    {
        if (!effect.always)
        {
            if (effect.max_time > 0f)
                effect.max_time -= Time.fixedDeltaTime;
            else
            {
                effect.max_time = -1f;
                RemoveEffect();
            }
        }
    }

    public void SetEffect()
    {
        if (effect.boost_type != BoostType.health && effect.boost_type != BoostType.god)
        {
            my_frame = Instantiate(frame_prefab, frame_parent, false);
            my_frame.transform.GetComponent<EffectFrame>().UpdateEffect(effect.boost_type);
        }

        switch (effect.boost_type)
        {
            case BoostType.speed:
                player.speedX += effect.modify;
                player.speedY += effect.modify;
                break;

            case BoostType.health:
                player.health += effect.modify;
                manager.UpdateHearts(false, my_boost_pos);
                Invoke("RemoveEffect", 0.1f);
                break;

            case BoostType.defuse_kit:
                player.defuse = true;
                break;

            case BoostType.cloud:
                player.max_gravity = Mathf.Clamp(player.max_gravity - effect.modify, 0f, 10f);
                break;

            case BoostType.god:
                player.god = true;
                break;
        }
    }

    public void RemoveEffect()
    {
        switch (effect.boost_type)
        {
            case BoostType.speed:
                player.speedX -= effect.modify;
                player.speedY -= effect.modify;
                break;

            case BoostType.defuse_kit:
                player.defuse = false;
                break;

            case BoostType.cloud:
                player.max_gravity = Mathf.Clamp(player.max_gravity + effect.modify, 0f, 10f);
                break;

            case BoostType.god:
                player.god = false;
                break;
        }

        if (manager != null)
        {
            manager.RemoveEffect(transform.GetComponent<Effect>());
        }

        if (my_frame != null)
            Destroy(my_frame);

        Destroy(gameObject);
    }

    public void UpdateEffect()
    {
        switch (effect.boost_type)
        {
            case BoostType.defuse_kit:
                if (effect.modify <= 0f)
                {
                    RemoveEffect();
                }
                break;
        }
    }
    */
}
