using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private List<Hero> _heroes = new List<Hero>();

    private Hero _hero;

    private int _index = 0;

    public void ShowFirst()
    {
        SelectHero();
        SetHeroActive(true);
    }

    public void ShowNext()
    {
        SetHeroActive(false);

        if (_index == _heroes.Count - 1)
        {
            _index = 0;
        }
        else
        {
            _index = _index + 1;
        }

        _hero = _heroes[_index];
        SetHeroActive(true);
    }

    public void ShowPrevious()
    {
        SetHeroActive(false);

        if (_index == 0)
        {
            _index = _heroes.Count - 1;
        }
        else
        {
            _index = _index - 1;
        }

        _hero = _heroes[_index];
        SetHeroActive(true);
    }

    public void SelectHero()
    {
        foreach (var hero in _heroes)
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
        SetHeroActive(false);
        _hero = _heroes.First(h => h.selected);
        SetHeroActive(true);
    }

    public void SetHeroActive(bool active)
    {
        _hero.gameObject.SetActive(active);
    }

    public object GetCurrentHeroStat(StatType type)
    {
        return _hero.GetStat(type);
    }
}
