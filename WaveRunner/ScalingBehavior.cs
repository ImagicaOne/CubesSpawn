using UnityEngine;

public class ScalingBehavior : MonoBehaviour
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
        _startState = transform.localScale;
        _endState = transform.localScale * _shift;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        var progress = Mathf.PingPong(_currentTime, _oneWayDuration) / _oneWayDuration;
        transform.localScale = Vector3.Lerp(_startState, _endState, progress);
    }
}
