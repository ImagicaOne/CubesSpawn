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
        _heroes[0].gameObject.SetActive(true);
        return _heroes[0];
    }

    public Hero GetCurrent()
    {
        return _heroes[_index];
    }

    public Hero GetNext()
    {
        _heroes[_index].gameObject.SetActive(false);

        if  (_index == _heroes.Count - 1)
        {            
            _index = 0;
        }
        else
        {
           _index = _index + 1;
        }
        _heroes[_index].gameObject.SetActive(true);
        return _heroes[_index];
    }

    public Hero GetPrevious()
    {
        _heroes[_index].gameObject.SetActive(false);
        if (_index == 0)
        {
            _index = _heroes.Count - 1;
        }
        else
        {
            _index = _index - 1;
        }
        _heroes[_index].gameObject.SetActive(true);
        return _heroes[_index];
    }
}
