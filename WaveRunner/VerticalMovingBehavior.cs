using UnityEngine;

public class VerticalMovingBehavior : MonoBehaviour
{
    [SerializeField]
    private int _shift;

    //[SerializeField]
    private Vector3 _startState;

    //[SerializeField]
    private Vector3 _endState;

    [SerializeField]
    private float _oneWayDuration;

    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
        _startState = transform.position;
        _endState = transform.position + transform.up * _shift;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        var progress = Mathf.PingPong(_currentTime, _oneWayDuration) / _oneWayDuration;
        transform.position = Vector3.Lerp(_startState, _endState, progress);

    }
}
