using System;

[Serializable]
public class Reward
{
    public Reward(RewardType type, int value)
    {
        Type = type;
        Value = value;
    }

    public string Id;
    public RewardType Type;
    public int Value;
    public int Price;
    public string ShopId;
    public string AdditionalArgument;
    public bool HasCoolDown;
    public int CoolDownMinutes;
}
