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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            drawingEvent.Invoke();
            drawingDone = true;
        }
        if (Input.GetMouseButtonUp(0) && drawingDone)
        {
            cargoEvent.Invoke();
        }
        if (_box.NeedToPut())
        {
            putEvent.Invoke();
        }
    }
}
