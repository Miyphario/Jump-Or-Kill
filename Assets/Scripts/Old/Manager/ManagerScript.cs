using System;
using UnityEngine;

public enum EventType
{
    no,
    christmass,
    winter,
    summer,
    spring,
    autumn
}

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript Instance { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        Instance = this;
    }

    public static void AddLeaderboards(int distance_traveled)
    {
        PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_distance_traveled, distance_traveled);
    }

    public static void UnlockAchievement(string id)
    {
        PlayGamesScript.UnlockAchievement(id);
    }

    public static bool AccessEvent(EventType checkEvent)
    {
        if (checkEvent != EventType.no)
        {
            DateTime curTime = DateTime.Now;

            switch (checkEvent)
            {
                case EventType.christmass:
                    if (curTime.Month == 12 || curTime.Month == 1)
                    {
                        return true;
                    }
                    break;

                case EventType.winter:
                    if (curTime.Month == 12 || curTime.Month == 1 || curTime.Month == 2)
                    {
                        return true;
                    }
                    break;

                case EventType.summer:
                    if (curTime.Month != 12 && curTime.Month != 1 && curTime.Month != 2)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    public static EventType GetCurrentEvent()
    {
        DateTime curTime = DateTime.Now;

        if (curTime.Month == 12 || curTime.Month == 1)
        {
            return EventType.christmass;
        }
        else if (curTime.Month == 2)
        {
            return EventType.winter;
        }
        else if (curTime.Month == 3 || curTime.Month == 4 || curTime.Month == 5)
        {
            return EventType.spring;
        }
        else if (curTime.Month == 6 || curTime.Month == 7 || curTime.Month == 8)
        {
            return EventType.summer;
        }
        else if (curTime.Month == 9 || curTime.Month == 10 || curTime.Month == 11)
        {
            return EventType.autumn;
        }

        return EventType.no;
    }

    public static EventType GetCurrentSeason()
    {
        DateTime curTime = DateTime.Now;

        if (curTime.Month == 12 || curTime.Month == 1 || curTime.Month == 2)
        {
            return EventType.winter;
        }
        else
        {
            return EventType.summer;
        }
    }

    public static Color MakeColor(float r, float g, float b, float a)
    {
        float red = r * 100f / 255f / 100f;
        float green = g * 100f / 255f / 100f;
        float blue = b * 100f / 255f / 100f;
        float alpha = a * 100f / 255f / 100f;

        return new Color(red, green, blue, alpha);
    }
}
