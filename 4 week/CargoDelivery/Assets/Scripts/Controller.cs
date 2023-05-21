using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _drawingEvent;

    [SerializeField]
    private UnityEvent _cargoEvent;

    [SerializeField]
    private UnityEvent _putEvent;

    [SerializeField]
    private BoxController _box;

    private bool _drawingDone = false;
    private bool _deliveryDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _drawingDone == false)
        {
            // LineController.DrawLine();
            _drawingEvent.Invoke();          
        }
        if (Input.GetMouseButtonUp(0) && _deliveryDone == false)
        {
            _drawingDone = true;
            // RopeController.Move()
            _cargoEvent.Invoke();
            _deliveryDone = true;

        }

        if (_box.NeedToPut())
        {       
            // BoxController.DropDown()
            _putEvent.Invoke();
        }
    }
}
