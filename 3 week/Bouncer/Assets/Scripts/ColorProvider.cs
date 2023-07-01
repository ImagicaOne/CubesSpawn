using UnityEngine;

[System.Serializable]
public class ColorProvider
{
    [SerializeField]
    private Color[] _colors;

    public Color GetRandomColor()
    {
        return _colors[Random.Range(0, _colors.Length)];
    }

    public Color[] GetAllColors()
    {
        return _colors;
    }
}
