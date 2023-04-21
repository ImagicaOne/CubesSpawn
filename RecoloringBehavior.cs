using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoloringBehavior : MonoBehaviour
{
    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;

    private float _recoloringDuration = 10f;
    private float _recoloringTime;

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
    }

    // Start is called before the first frame update
    
    private void Start()
    {
        //var randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
        //GetComponent<Renderer>().material.color = randomColor;
    }

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void Update()
    {
        _recoloringTime += Time.deltaTime;
        var progress = _recoloringTime / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (_recoloringTime >= _recoloringDuration)
        {
            _recoloringTime = 0f;
            GenerateNextColor();
        }

    }
}
