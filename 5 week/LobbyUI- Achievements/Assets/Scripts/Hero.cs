using System;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //public bool isShowing;
    public bool bought;
    public bool selected;

    [SerializeField]
    private string _name;
    [SerializeField]
    private HeroType _type;
    [SerializeField]
    private int _number;
    [SerializeField]
    private string _rating;
    [SerializeField]
    private int _price;

    [SerializeField]
    private Sprite _icon;

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _attack;
    [SerializeField]
    private int _defense;
    [SerializeField]
    private int _speed;

    public object GetStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.health:  
                return _health;
            case StatType.attack:
                return _attack;
            case StatType.defense:
                return _defense;
            case StatType.speed:
                return _speed;
            case StatType.heroName:
                return _name;
            case StatType.type:
                return _type;
            case StatType.number:
                return _number;
            case StatType.rating:
                return _rating;
            case StatType.price:
                return _price;
            case StatType.icon:
                return _icon;
            default:
                throw new ArgumentException("Stat type is not found!");
        }
    }
        

        
}
