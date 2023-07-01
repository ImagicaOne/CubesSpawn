using System;
using UnityEngine;
using UnityEngine.Events;

public class CubeController : MonoBehaviour
{
    private Action _cubeMoved;

    private float _power = 10;

    private Vector3 _startPosition = new Vector3(0, 0.7f, 0);

    private Rigidbody _rb;

    public void Initialize(Action cubeMoved)
    {
        _rb = GetComponent<Rigidbody>();
        transform.position = _startPosition;
        _cubeMoved += cubeMoved;
    }

    public void MoveToStartPosition()
    {
        transform.position = _startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out _hitInfo))
            {
                _rb.AddForce((_hitInfo.point - transform.position).normalized * _power, ForceMode.Impulse);
                _cubeMoved.Invoke();
            }
        }                
    }
}
