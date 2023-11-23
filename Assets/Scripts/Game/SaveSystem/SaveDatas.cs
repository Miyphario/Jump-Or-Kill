using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int money;
    public int snowballs;
    public int gems;
    public int adsMoney;

    public PlayerData(int money, int snowballs, int gems, int adsMoney)
    {
        this.money = money;
        this.snowballs = snowballs;
        this.gems = gems;
        this.adsMoney = adsMoney;
    }
}

[Serializable]
public class ScoreData
{
    public DateTime dateScore;
    public int distanceTraveled;
    public int kills;
    public float[] lastPosition = new float[2];
    public int moneyTaked;
    public int snowballsTaked;
    public int gemsTaked;

    public ScoreData(DateTime dateScore, int distanceTraveled, int kills, float[] lastPosition, int moneyTaked, int snowballsTaked, int gemsTaked)
    {
        this.dateScore = dateScore;
        this.distanceTraveled = distanceTraveled;
        this.kills = kills;
        this.lastPosition = lastPosition;
        this.moneyTaked = moneyTaked;
        this.snowballsTaked = snowballsTaked;
        this.gemsTaked = gemsTaked;
    }
}

[Serializable]
public class SaveObjectData
{
    public bool purchased;
    public bool selected;

    public SaveObjectData(bool purchased, bool selected)
    {
        this.purchased = purchased;
        this.selected = selected;
    }
}