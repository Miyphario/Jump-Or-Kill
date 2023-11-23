using System;

[Serializable]
public class BoostData
{
    public string render_name;
    public string boost_name;
    public BoostType boost_type;
    public float max_time;
    public float modify;
    public bool always;
    public bool buyed;
    public PriceType price_type;
    public int price;

    public BoostData(BoostType boost_type, float max_time, float modify, bool always)
    {
        this.boost_type = boost_type;
        this.max_time = max_time;
        this.modify = modify;
        this.always = always;
    }
}
