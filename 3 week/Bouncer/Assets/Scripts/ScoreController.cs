using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI[] _texts;

    private void Awake()
    {
        _texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void IncreaseScore(Color color)
    {
        foreach (var text in _texts)
        {
            if (color == Color.white)
            {
                if (text.name.Contains("move", StringComparison.InvariantCultureIgnoreCase))
                {
                    text.text = (Convert.ToInt32(text.text) + 1).ToString();
                }              
            }
            else if (text.name.Contains(ColorProvider.GetColorName(color), StringComparison.InvariantCultureIgnoreCase))
            {
                text.text = (Convert.ToInt32(text.text) - 1).ToString();
            }             
        }    
    }

    public void SetInitialScore(Color color)
    {
        foreach (var text in _texts)
        {
            if (text.name.Contains(ColorProvider.GetColorName(color), StringComparison.InvariantCultureIgnoreCase))
            {
                text.text = (Convert.ToInt32(text.text) + 1).ToString();
            }
        }
    }
}
