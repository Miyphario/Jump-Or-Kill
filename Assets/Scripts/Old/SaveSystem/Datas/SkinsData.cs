using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkinsData
{
    public List<Skin> skins;

    public SkinsData(List<Skin> skins)
    {
        this.skins = skins;
    }
}
