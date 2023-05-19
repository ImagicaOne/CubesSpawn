using UnityEngine;

public class CubeController2Points : MonoBehaviour
{
    [SerializeField]
    private Transform _startState;

    [SerializeField]
    private Transform _endState;

    [SerializeField]
    private float _oneWayDuration;

    [SerializeField]
    private float _sleepTime = 3f;

    private Color _startColor;
    private Color _finalColor;
    private Renderer _renderer;

    private float _currentTime;
    private float _timeForCheckPosition = 0;
    private float _timeForCheckSleep = 0;

    void Start()
    {
        _startColor = _startState.GetComponent<Renderer>().material.color;
        _finalColor = _endState.GetComponent<Renderer>().material.color;
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        _timeForCheckPosition += Time.deltaTime;

        if (_timeForCheckPosition >= _oneWayDuration)
		//if (Vector3.Distance(transform.position, _endState.position) == distance || Vector3.Distance(transform.position, _startState.position) == distance)
        {
                _timeForCheckSleep += Time.deltaTime;
            if (_timeForCheckSleep >= _sleepTime)
            {
                ChangeProperties();
                _timeForCheckSleep = 0f;
                _timeForCheckPosition = 0f;
            }         
        }
        else
        {
            ChangeProperties();
        }       
    }

    private void ChangeProperties()
    {
        _currentTime += Time.deltaTime;
        var progress = Mathf.PingPong(_currentTime, _oneWayDuration) / _oneWayDuration;
        _renderer.material.color = Color.Lerp(_startColor, _finalColor, progress);
        transform.position = Vector3.Lerp(_startState.position, _endState.position, progress);
        transform.rotation = Quaternion.Lerp(_startState.rotation, _endState.rotation, progress);
        transform.localScale = Vector3.Lerp(_startState.localScale, _endState.localScale, progress);
    }
}
