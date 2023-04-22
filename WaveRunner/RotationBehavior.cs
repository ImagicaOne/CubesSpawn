using UnityEngine;

public class RotationBehavior : MonoBehaviour
{
    [SerializeField]
    private int _Speed;

    private float _currentTime;

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        transform.Rotate(0, 0, _Speed);
    }
}
