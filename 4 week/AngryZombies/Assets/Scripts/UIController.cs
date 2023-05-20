using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_text.text.Equals("0"))
        {
            ShowWinScreen();
        }
    }

    public void ReduceObjectsCount()
    {
        _text.text = (Convert.ToInt32(_text.text) - 1).ToString();
    }

    private void ShowWinScreen()
    {
        GetComponentInChildren<WinScreen>(true).gameObject.SetActive(true);
    }
}
