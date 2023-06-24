using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroProvider : MonoBehaviour
{
    [SerializeField]
    private List<Hero> _heroes = new List<Hero>();

    private Hero _hero;

    private int _index = 0;

    public void ShowFirst()
    {
        SelectHero();
        SetActive(true);
    }

    public void ShowNext()
    {
        SetActive(false);

        if  (_index == _heroes.Count - 1)
        {            
            _index = 0;
        }
        else
        {
           _index = _index + 1;
        }

        _hero = _heroes[_index];
        SetActive(true);
    }

    public void ShowPrevious()
    {
        SetActive(false);

        if (_index == 0)
        {
            _index = _heroes.Count - 1;
        }
        else
        {
            _index = _index - 1;
        }

        _hero = _heroes[_index];
        SetActive(true);
    }

    public void SelectHero()
    {
        foreach(var hero in _heroes)
        {
            hero.selected = false;
        }

        if (_hero != null)
        {
            _hero.selected = true;
        }
        else
        {
            _hero = _heroes[0];
        }       
    }

    public void SetBought()
    {
        _hero.bought = true;
    }

    public bool IsBought()
    {
        return _hero.bought;
    }

    public void ShowSelected()
    {
        SetActive(false);
        _hero = _heroes.First(h => h.selected);
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        _hero.gameObject.SetActive(active);
    }

    public object GetCurrentHeroStat(StatType type)
    {
        return _hero.GetStat(type);
    }
}
