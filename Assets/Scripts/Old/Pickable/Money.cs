using System.Collections.Generic;
using UnityEngine;

public class Money : BasePickable
{
    /*
    public int money;
    public PriceType type = PriceType.money;
    public Sprite gem_sprite;
    public Sprite snowball_sprite;

    public List<Sprite> money_sprites = new List<Sprite>();

    private void ChangeMoneySprite() //Change sprite for money/coins
    {
        if (type == PriceType.money)
        {
            if (money_sprites.Count > 0)
            {
                foreach (Sprite s in money_sprites)
                {
                    if (s != null)
                    {
                        string mname = "money" + money;
                        if (s.name == mname)
                        {
                            ChangeSprite(s);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void UpdateMoneySprite() //Change sprite for money/coins, gems and snowballs
    {
        switch (type)
        {
            case PriceType.money:
                ChangeMoneySprite();
                audio_prefix = "money";
                break;

            case PriceType.gems:
                ChangeSprite(gem_sprite);
                audio_prefix = "gem";
                min_pitch = 0.9f;
                max_pitch = 1.1f;
                break;

            case PriceType.snowballs:
                ChangeSprite(snowball_sprite);
                audio_prefix = "snowball";
                break;
        }

        ChangeAudio();
    }

    public override void Action(BaseEntity entity)
    {
        switch (type)
        {
            case PriceType.money:
                entity.money += money;
                break;

            case PriceType.gems:
                entity.gems += money;
                break;

            case PriceType.snowballs:
                entity.snowballs += money;
                break;
        }

        entity.MoneyPicked(sprite_rend.sprite, type);

        base.Action(entity);
    }
    */
}
