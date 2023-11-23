using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [HideInInspector]
    public bool anim_played = false;

    [HideInInspector]
    public RectTransform rect_transform;
    [HideInInspector]
    public RectTransform finish_transform;
    [HideInInspector]
    public Transform parent_transform;

    private float speed = 0.05f;
    private Vector2 my_size = new Vector2(41.5f, 36f);
    [HideInInspector]
    public RectTransform safe_area_transform;

    public int my_number;

    private void Awake()
    {
        rect_transform = transform.GetComponent<RectTransform>();
        rect_transform.sizeDelta = my_size;
    }

    private void Update()
    {
        if (!anim_played)
        {
            float posX = finish_transform.anchoredPosition.x;
            float posY = finish_transform.anchoredPosition.y;

            Vector2 finishPos = new Vector2(posX - finish_transform.sizeDelta.x - (my_size.x / 2) /*- (my_number > 1 ? 2 : 0)*/, posY);
            //Vector2 finishPos = new Vector2(posX - (my_number * my_size.x / 2) - my_number > 1 ? 2 : 0, posY);

            float dist = Vector2.Distance(rect_transform.anchoredPosition, finishPos);

            Vector3 tt = rect_transform.anchoredPosition;

            if (dist > 0.5f)
            {
                rect_transform.anchoredPosition = Vector2.Lerp(tt, finishPos, speed);
            }
            else
            {
                anim_played = true;
                transform.SetParent(finish_transform);
            }
        }
    }
}
