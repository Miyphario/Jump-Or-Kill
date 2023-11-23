using UnityEngine;
using UnityEngine.UI;

public class EffectFrame : MonoBehaviour
{
    public Image effect_sprite;

    public Sprite speed_sprite;
    public Sprite defuse_kit_sprite;
    public Sprite cloud_sprite;

    [HideInInspector]
    public RectTransform my_effect_transform;

    private void Awake()
    {
        my_effect_transform = transform.GetChild(0).transform.GetComponent<RectTransform>();
    }

    public void UpdateEffect(BoostType effect)
    {
        switch (effect)
        {
            case BoostType.speed:
                Vector2 spSize = new Vector2(speed_sprite.rect.width / 2, speed_sprite.rect.height / 2);
                my_effect_transform.sizeDelta = spSize;
                effect_sprite.sprite = speed_sprite;
                break;

            case BoostType.defuse_kit:
                Vector2 dkSize = new Vector2(defuse_kit_sprite.rect.width / 2, defuse_kit_sprite.rect.height / 2);
                my_effect_transform.sizeDelta = dkSize;
                effect_sprite.sprite = defuse_kit_sprite;
                break;

            case BoostType.cloud:
                Vector2 clSize = new Vector2(cloud_sprite.rect.width / 2, cloud_sprite.rect.height / 2);
                my_effect_transform.sizeDelta = clSize;
                effect_sprite.sprite = cloud_sprite;
                break;
        }
    }
}
