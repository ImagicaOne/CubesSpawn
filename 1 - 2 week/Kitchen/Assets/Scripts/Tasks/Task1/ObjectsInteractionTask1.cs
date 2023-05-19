using System.Collections;
using UnityEngine;

public class ObjectsInteractionTask1 : MonoBehaviour
{
    // TODO: Получите доступ к объекту со скриптом Refrigerator при помощи атрибута [SerializeField]
    // TODO: При нажатии на кнопку 1 на клавиатуре вызывайте у него метод Interact

    [SerializeField]
    private Refrigerator fridgeObject;

    [SerializeField]
    private GameObject chicken;

    private float _speed = 1.5F;
    [SerializeField]
    private Transform _startMarker;
    [SerializeField]
    private Transform _endMarker;

    private float _currentTime;
    bool open = false;

    public float delay = 1;

    float _time;
    float _timeDelay;

    private void Start()
    {
        _time = 0f;
        _timeDelay = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fridgeObject.Interact();
            open = !open;
        }

        if (open)
        {
            _time = _time + Time.deltaTime;
            if (_time >= _timeDelay)
            {
                //if (open)
                //{
                _currentTime += Time.deltaTime;
                chicken.transform.position = Vector3.Lerp(_startMarker.position, _endMarker.position, _currentTime);
                //}

                if (chicken.transform.position == _endMarker.position)
                {
                    chicken.transform.Rotate(0, _speed * _currentTime, 0);
                }
            }
        }

    }
}
