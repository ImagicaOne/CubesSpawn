using UnityEngine;

public class RecoloringBehavior : MonoBehaviour
{
    private Color _currentColor;
    private Color _nextColor;
    private Renderer _renderer;

    [SerializeField]
    private float _recoloringDuration;
    [SerializeField]
    private float _recoloringDelay;

    private float _timer;

    private void GenerateNextColor()
    {
        _currentColor = _renderer.material.color;
        _nextColor = Random.ColorHSV();
    }

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        var progress = _timer / _recoloringDuration;
        _renderer.material.color = Color.Lerp(_currentColor, _nextColor, progress);

        if (_timer >= _recoloringDuration + _recoloringDelay)
        {
            _timer = 0f;
            GenerateNextColor();
        }

    }
}
