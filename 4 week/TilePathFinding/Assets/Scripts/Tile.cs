using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isObstacle = false;

    [SerializeField]
    private Color _allowingColor;
    [SerializeField]
    private Color _forbiddingColor;

    [SerializeField]
    private Color _hilightColor;

    private List<Material> _materials = new();

    public Vector2Int placeInMap;


    private void Awake()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in renderers)
        {
            _materials.Add(meshRenderer.material);
        }
    }

    public void SetColor(bool available)
    {
        if (available)
        {
            foreach (var material in _materials)
            {
                material.color = _allowingColor;
            }
        }
        else
        {
            foreach (var material in _materials)
            {
                material.color = _forbiddingColor;
            }
        }
    }

    public void ResetColor()
    {
        foreach (var material in _materials)
        {
            material.color = Color.white;
        }
    }

    public void ChangeColor(Color color)
    {
        // top of the tile
        gameObject.GetComponentsInChildren<Renderer>()[1].material.color = color;
    }
}