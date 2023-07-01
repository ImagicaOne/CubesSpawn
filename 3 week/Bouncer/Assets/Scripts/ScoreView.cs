using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Image[] _cylinderColors;

    [SerializeField]
    private TextMeshProUGUI[] _enemiesCountLabel; // ћассив, содержащий текст с количеством цилиндров каждого цвета

    [SerializeField]
    private TextMeshProUGUI _playerMovementCountLabel; // “екст с количеством передвижений кубика

    public void Initialize(ColorProvider colorProvider)
    {
        var colors = colorProvider.GetAllColors();

        for (var i = 0; i < colors.Length; i++)
        {
            var color = colors[i];
            _cylinderColors[i].color = color;
        }
    }

    public void SetPlayerMovementsCount()
    {
        _playerMovementCountLabel.text = (Convert.ToInt32(_playerMovementCountLabel.text) + 1).ToString();
    }

    public void SetCountOfEnemiesWithColor(Color color, int count)
    {
        for (var i = 0; i < _cylinderColors.Length; i++)
        {
            if (color == _cylinderColors[i].color)
            {
                _enemiesCountLabel[i].text = (Convert.ToInt32(_enemiesCountLabel[i].text) + count).ToString();
                break;
            }
        }
    }
}

