using UnityEngine;

public class HighlightBehaviour : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layer;

    private GameObject _gameObject;

    private Color _newColor = new Color(0, 204, 102);
    private Color _initialColor;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, 1000f, _layer))
        {
            if (_gameObject != hitInfo.collider.gameObject)
            {
                ChangeColor(_gameObject, _initialColor);
                _gameObject = hitInfo.collider.gameObject;
                _initialColor = _gameObject.GetComponent<Renderer>().material.color;
                ChangeColor(_gameObject, _newColor);
            }                                         
        }
        else
        {
            Debug.Log("Change back");
            ChangeColor(_gameObject, _initialColor);
        }

    }

    private void ChangeColor(GameObject gameObject, Color color)
    {
        if (_gameObject != null && _initialColor != null)
            gameObject.GetComponent<Renderer>().material.color = color;
    }
}
