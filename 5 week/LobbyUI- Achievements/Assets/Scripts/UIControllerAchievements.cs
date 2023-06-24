using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerAchievements : MonoBehaviour
{
    [SerializeField]
    private Button _left;

    [SerializeField]
    private Button _right;

    [SerializeField]
    private Button _select;

    [SerializeField]
    private GameObject[] _lobbyCanvaces;

    [SerializeField]
    private GameObject[] _choosingHeroCanvaces;

    [SerializeField]
    private GameObject _priceButton;

    [SerializeField]
    private GameObject _selectButton;

    [SerializeField]
    private GameObject _money;

    [SerializeField]
    private HeroProvider _heroProvider;

    private List<HeroDependElement> _heroDependElements = new List<HeroDependElement>();

    public void Start()
    {
        foreach (var element in _choosingHeroCanvaces)
        {
            _heroDependElements.AddRange(element.GetComponentsInChildren<HeroDependElement>(false));
        }

    }

    public void PressHeroes()
    {
        SwitchScreen(false, true);
    }

    public void GoToMainScreen()
    {
        SwitchScreen(true, false);

        
    }

    public void PressLeft()
    {
        
    }

    public void PressRight()
    {
       
    }

    public void ControlBuyButtonVisibility()
    {
        if (_heroProvider.IsBought())
        {
            _priceButton.gameObject.SetActive(false);
            _selectButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            _priceButton.gameObject.SetActive(true);
            _selectButton.GetComponent<Button>().interactable = false;

        }
    }

    public void SelectHero()
    {
        _heroProvider.SelectHero();
        SwitchScreen(true, false);
    }

    public void BuyHero()
    {
        _heroProvider.SetBought();
        _priceButton.gameObject.SetActive(false);
        var price = _priceButton.GetComponentInChildren<TextMeshProUGUI>();
        var money = _money.GetComponent<TextMeshProUGUI>();
        money.text = (Convert.ToDecimal(money.text.Replace(",", string.Empty)) - Convert.ToInt32(price.text)).ToString("#,#");
        _selectButton.GetComponent<Button>().interactable = true;
    }

    private void LoadHeroData()
    {
        foreach (var element in _heroDependElements)
        {
            element.Change(_heroProvider.GetCurrentHeroStat(element.statToChange));
        }
    }

    private void SwitchScreen(bool first, bool second)
    {
        foreach (var element in _lobbyCanvaces)
        {
            element.SetActive(first);
        }
        foreach (var element in _choosingHeroCanvaces)
        {
            element.SetActive(second);
        }
    }
}
