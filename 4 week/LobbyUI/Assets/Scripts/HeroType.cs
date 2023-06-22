using System.ComponentModel;

public enum HeroType
{
    [Description("No weapon hero")]
    NoWeaponHero,

    [Description("Archer")]
    Archer,

    [Description("Wizard")]
    Wizard,

    [Description("Double sword hero")]
    DoubleSword,

    [Description("Defender")]
    SwordShield,

    [Description("Sword master")]
    TwoHandsSword
}
