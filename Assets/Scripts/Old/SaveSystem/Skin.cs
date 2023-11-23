using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkinCategory
{
    standart,
    usual,
    christmass,
    all,
    buyed,
    noPrice
}

public enum PriceType
{
    money,
    gems,
    ads,
    snowballs,
    noPrice
}

[Serializable]
public class Skin
{
    public string render_name;
    public string skin_name;
    public int price;
    public bool buyed;
    public bool selected;
    public PriceType price_type;
    public SkinCategory category;

    public EventType myEvent = EventType.no;

    public string noPriceDesc;

    public Skin(string skin_name, int price, bool buyed, bool selected, PriceType price_type, SkinCategory category, EventType myEvent, string noPriceDesc)
    {
        this.skin_name = skin_name;
        this.price = price;
        this.buyed = buyed;
        this.selected = selected;
        this.price_type = price_type;
        this.category = category;
        this.myEvent = myEvent;
        this.noPriceDesc = noPriceDesc;
    }
}
