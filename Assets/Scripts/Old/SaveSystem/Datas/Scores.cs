using System;
using UnityEngine;

[Serializable]
public class Scores
{
    public DateTime save_time;
    public int distance_traveled;
    public int kills;
    public float[] last_position = new float[2];
    public int money_taked;

    public Scores(Creature entity)
    {
        /*
        save_time = DateTime.Now;
        distance_traveled = Mathf.RoundToInt(entity.walk_distance);
        kills = entity.kills;
        last_position[0] = entity.transform.position.x;
        last_position[1] = entity.road_pos[entity.line - 1].position.y;
        money_taked = entity.money;
        */
    }
}
