using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private UnityEvent drawingEvent;

    [SerializeField]
    private UnityEvent cargoEvent;

    [SerializeField]
    private UnityEvent putEvent;

    [SerializeField]
    private BoxController _box;

    private bool drawingDone = false;
    private bool deliveryDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && drawingDone == false)
        {
            drawingEvent.Invoke();          
        }
        if (Input.GetMouseButtonUp(0) && deliveryDone == false)
        {
            drawingDone = true;
            cargoEvent.Invoke();
            deliveryDone = true;

        }
        if (_box.NeedToPut())
        {           
            putEvent.Invoke();
        }
    }
}
