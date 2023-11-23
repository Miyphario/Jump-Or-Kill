using System;

[Serializable]
public class GameData
{
    public int money;
    public int gems;
    public int snowballs;

    public GameData(Creature entity)
    {
        /*
        money = entity.money;
        gems = entity.gems;
        snowballs = entity.snowballs;
        */
    }
}
