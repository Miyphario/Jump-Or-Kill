using UnityEngine;

public enum BoostType
{
    speed,
    health,
    defuse_kit,
    cloud,
    god
}

public class Boost : BasePickable
{
    public Sprite faid_kit_sprite, speed_sprite, defuse_kit_sprite, cloud_sprite;

    public BoostData my_data;

    public override void Action(Creature entity)
    {
        /*
        if (entity.AddEffect(my_data, transform.position))
        {
            if (audio_source != null)
            {
                audio_source.pitch = Random.Range(0.85f, 1.15f);
                base.Action(entity);
            }
        }*/
    }

    public void UpdateSprite()
    {
        switch (my_data.boost_type)
        {
            case BoostType.speed:
                ChangeSprite(speed_sprite);
                break;

            case BoostType.health:
                ChangeSprite(faid_kit_sprite);
                break;

            case BoostType.defuse_kit:
                ChangeSprite(defuse_kit_sprite);
                break;

            case BoostType.cloud:
                ChangeSprite(cloud_sprite);
                break;
        }
    }
}
