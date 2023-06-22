using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Button _left;

    [SerializeField]
    private Button _right;

    [SerializeField]
    private Button _select;

    [SerializeField]
    private GameObject _lobbyCanvac;

    [SerializeField]
    private GameObject _lobbyBackCanvac;

    [SerializeField]
    private GameObject _choosingCanvac;

    [SerializeField]
    private GameObject _priceButton;

    [SerializeField]
    private GameObject _money;

    [SerializeField]
    private HeroProvider _heroProvider;

    private ChangingUIElement[] _elementsToChange;

    private Hero _hero;

    public void Start()
    {
        _elementsToChange = _choosingCanvac.GetComponentsInChildren<ChangingUIElement>(false);
        _hero = _heroProvider.GetFirst();
        ControlSelectButton();
        LoadHeroData();
    }

    public void PressHeroes()
    {
        _lobbyCanvac.SetActive(false);
        _lobbyBackCanvac.SetActive(false);
        _choosingCanvac.SetActive(true);
    }

    public void PressSelect()
    {
        _lobbyCanvac.SetActive(true);
        _lobbyBackCanvac.SetActive(true);
        _choosingCanvac.SetActive(false);
    }

    public void PressLeft()
    {
        _hero = _heroProvider.GetPrevious();
        ControlSelectButton();
        LoadHeroData();
    }

    public void PressRight()
    {
        _hero = _heroProvider.GetNext();
        ControlSelectButton();
        LoadHeroData();
    }

    public void ControlSelectButton()
    {
        if (_hero.bought)
        {
            _priceButton.gameObject.SetActive(false);    
        }
        else
        {
            _priceButton.gameObject.SetActive(true);
        }
    }

    public void BuyHero()
    {
        _hero.bought = true;
        _priceButton.gameObject.SetActive(false);
        var price = _priceButton.GetComponentInChildren<TextMeshProUGUI>();
        var money = _money.GetComponent<TextMeshProUGUI>();
        money.text = (Convert.ToDecimal(money.text.Replace(",", string.Empty)) - Convert.ToInt32(price.text)).ToString("#,#");
    }

    private void LoadHeroData()
    {
        foreach (var element in _elementsToChange)
        {
            element.Change(_hero.GetStat(element.statToChange));
        }
    }
}
