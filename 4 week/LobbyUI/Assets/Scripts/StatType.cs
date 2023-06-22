using System.ComponentModel;

public enum StatType
{
    [Description("Hero name")]
    heroName = 0,

    [Description("Health")]
    health = 1,

    [Description("Attack")]
    attack = 2,

    [Description("Defense")]
    defense = 3,

    [Description("Speed")]
    speed = 4,

    [Description("Hero type")]
    type = 5,

    [Description("Hero number")]
    number = 6,

    [Description("Hero rating")]
    rating = 7,

    [Description("Hero price")]
    price = 8
}
