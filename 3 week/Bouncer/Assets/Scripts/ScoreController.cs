using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Color, int> _onScoreChange;

    [SerializeField]
    private UnityEvent _onMoveChange;

    private ColorProvider _colorProvider;

    private Dictionary<Color, int> _scores = new();


    public void Initialize(ColorProvider colorsProvider)
    {
        _colorProvider = colorsProvider;
        var colors = _colorProvider.GetAllColors();

        // Инициализируем словарь.
        foreach (var color in colors)
        {
            _scores.Add(color, 0);
        }
    }

    public void SetInitialScore(Color color)
    {
        if (color != Color.white)
        {
            _onScoreChange.Invoke(color, 1);
        }
          
    }

    public void UpdateScore(Color color)
    {
        if (color != Color.white)
        {
            _onScoreChange.Invoke(color, -1);
        }
        else
        {
            _onMoveChange.Invoke();
        }        
        
    }
     
}
