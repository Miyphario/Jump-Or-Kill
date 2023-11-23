using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZOrder : MonoBehaviour
{
    public List<GameObject> sprites_to_change = new List<GameObject>();
    public bool has_one;
    public bool make_maskable;
    public bool mask_outside;

    public bool refresh;

    private void Start()
    {
        UpdateOrder();
    }

    public void UpdateOrder()
    {
        if (!has_one)
        {
            if (transform.parent.name == "Player")
            {
                foreach (GameObject n in sprites_to_change)
                {
                    SpriteRenderer sp = n.GetComponent<SpriteRenderer>();
                    if (sp != null)
                    {
                        sp.sortingLayerName = "Player";
                    }
                }
            }

            if (make_maskable)
            {
                SpriteMaskInteraction mi = mask_outside ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.VisibleInsideMask;
                foreach (GameObject n in sprites_to_change)
                {
                    SpriteRenderer sp = n.GetComponent<SpriteRenderer>();

                    if (sp != null)
                    {
                        sp.maskInteraction = mi;
                    }
                }
            }
        }
        else
        {
            SpriteRenderer sp = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
            if (sp != null)
            {
                sp.sortingLayerName = "Player";
            }
        }
    }

    private void Update()
    {
        if (refresh)
        {
            UpdateOrder();
            refresh = false;
        }
    }
}
