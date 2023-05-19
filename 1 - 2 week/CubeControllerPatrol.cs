using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControllerPatrol : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _states;

    [SerializeField]
    private float _oneWayDuration = 10f;

    [SerializeField]
    private float _sleepTime = 10f;

    private float _currentTime;
    
    private int i = 0;

    private Renderer _renderer;

    private float _timeForCheckSleep = 0;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        var start = _states[i];
        var end = (i + 1 == _states.Count) ? _states[0] : _states[i + 1];
        var distance = Vector3.Distance(start.position, end.position);
        var travelTime = distance / _oneWayDuration;

        if (Vector3.Distance(transform.position, end.position) == distance)
        {
            _timeForCheckSleep += Time.deltaTime;
            if (_timeForCheckSleep >= _sleepTime)
            {
                _timeForCheckSleep = 0f;
                ChangeProperties(start, end, travelTime);    
            }
        }
        else
        {
            ChangeProperties(start, end, travelTime);
        }
    }
        
    private void ChangeProperties(Transform start, Transform end, float travelTime)
    {
        _currentTime += Time.deltaTime;    
        var progress = _currentTime / travelTime;

        transform.position = Vector3.Lerp(start.position, end.position, progress);
        transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, progress);
        transform.localScale = Vector3.Lerp(start.localScale, end.localScale, progress);
        _renderer.material.color = Color.Lerp(start.GetComponent<Renderer>().material.color, end.GetComponent<Renderer>().material.color, progress);

        if (_currentTime >= travelTime)
        {
            _currentTime = 0f;
            if (i == _states.Count - 1)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }
    }
}
