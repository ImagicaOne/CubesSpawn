using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int2 : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private float distanceToInteract;
    [SerializeField] private Transform pointToStartRay;

    [SerializeField] private LayerMask interactableLayer;

    private Camera _camera;

    private InteractableItem _previousFocus;

    [SerializeField] private Transform inventoryHolder;
    private GameObject _heldObject;
    private bool _canThrow;
    [SerializeField] private Transform positionForPicksObjects;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        ControlRayDoor();
        ControlObjects();

        if (Input.GetMouseButton(0) && _canThrow)
        {
            _canThrow = false;
            TrowObject();
        }
    }

    private void TrowObject()
    {
        InteractableItem throwObject = inventoryHolder.gameObject.GetComponentInChildren<InteractableItem>();
        if (throwObject != null)
        {
            throwObject.GetComponent<Rigidbody>().useGravity = true;
            throwObject.GetComponent<Rigidbody>().isKinematic = false;
            throwObject.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            throwObject.transform.parent = null;
        }
    }

    private void ControlRayDoor()
    {
        Ray ray = new Ray(_camera.transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, distanceToInteract, doorLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Door door = hit.collider.gameObject.GetComponent<Door>();
                if (door != null)
                {
                    door.SwitchDoorState();
                }
            }
        }
    }

    private void ControlObjects()
    {
        Ray ray = new Ray(_camera.transform.position, transform.forward);
        Debug.DrawRay(_camera.transform.position, transform.forward * distanceToInteract, Color.black);

        if (Physics.Raycast(ray, out RaycastHit hit, distanceToInteract, interactableLayer))
        {
            InteractableItem interactableItem = hit.collider.gameObject.GetComponent<InteractableItem>();
            if (interactableItem != null)
            {
                if (interactableItem != _previousFocus)
                {

                    if (_previousFocus != null)
                    {
                        _previousFocus.RemoveFocus();
                    }

                    interactableItem.SetFocus();
                    _previousFocus = interactableItem;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    TrowObject();
                    hit.collider.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                    hit.transform.parent = inventoryHolder;
                    hit.transform.position = positionForPicksObjects.position;
                    _canThrow = true;
                }
            }
        }
        else
        {

            if (_previousFocus != null)
            {
                _previousFocus.RemoveFocus();
                _previousFocus = null;
            }
        }
    }
}