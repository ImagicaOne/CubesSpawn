using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroProvider : MonoBehaviour
{
    [SerializeField]
    private List<Hero> _heroes = new List<Hero>();

    private int _index = 0;

    public Hero GetFirst()
    {
        SetActive(_heroes[0], true);
        return _heroes[0];
    }

    public Hero GetCurrent()
    {
        return _heroes[_index];
    }

    public Hero GetNext()
    {
        if  (_index == _heroes.Count - 1)
        {            
            _index = 0;
        }
        else
        {
           _index = _index + 1;
        }
        return _heroes[_index];
    }

    public Hero GetPrevious()
    {
        if (_index == 0)
        {
            _index = _heroes.Count - 1;
        }
        else
        {
            _index = _index - 1;
        }
        return _heroes[_index];
    }

    public void SelectHero(Hero newSelectedHero)
    {
        foreach(var hero in _heroes)
        {
            hero.selected = false;
        }
        newSelectedHero.selected = true;
    }

    public void SetBought(Hero hero)
    {
        hero.bought = true;
    }

    public bool IsBought(Hero hero)
    {
        return hero.bought;
    }

    public Hero GetSelected()
    {
        return _heroes.First(h => h.selected);
    }

    public void SetActive(Hero hero, bool active)
    {
        hero.gameObject.SetActive(active);
    }
}
