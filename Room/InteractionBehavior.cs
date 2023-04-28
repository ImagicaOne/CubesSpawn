using UnityEngine;
using UnityEngine.Events;

public class InteractionBehavior : MonoBehaviour
{
    [SerializeField]
    private LayerMask _doorsLayer;

    [SerializeField]
    private LayerMask _InteractiveItemsLayer;

    private RaycastHit _hitInfo;

    private GameObject _currentGameObject;

    private int _distance = 5;

    void FixedUpdate()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        // _doorsLayer processing
        if (Physics.Raycast(ray, out _hitInfo, _distance, _doorsLayer))
        {           
            if (Input.GetKeyDown(KeyCode.E))
            {
                Door door = _hitInfo.collider.gameObject.GetComponent<Door>();
                door.SwitchDoorState();
            }         
        }

        // _InteractiveItemsLayer processing
        if (Physics.Raycast(ray, out _hitInfo, _distance, _InteractiveItemsLayer))
        {            
            // if target goes from one object to another, unhighlight previous object
            if (_currentGameObject!= null && _currentGameObject != _hitInfo.collider.gameObject)
            {
                ClearHighlighting(_currentGameObject);
            }

            _currentGameObject = _hitInfo.collider.gameObject;
            Highlight(_currentGameObject);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (transform.Find("InventoryHolder").childCount > 0)
                {
                    ThrowAll();
                }
                LiftItem();
            }          
        }
        else
        {
            if (_currentGameObject != null)
            {
                ClearHighlighting(_currentGameObject);
            }         
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowAll();
        }
    }

    private void LiftItem()
    {
        _currentGameObject.transform.SetParent(transform.Find("InventoryHolder"));
        _currentGameObject.transform.localPosition = new Vector3(0, 0, 0);
        _currentGameObject.GetComponent<Rigidbody>().isKinematic = true;
        ClearHighlighting(_currentGameObject);
    }

    private void ThrowAll()
    {
        InteractableItem[] allChildren = GetComponentsInChildren<InteractableItem>();
        foreach (InteractableItem child in allChildren)
        {
            child.transform.SetParent(null);
            child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            child.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
        }
    }

    private void ClearHighlighting(GameObject gameObject)
    {
        InteractableItem intItem = gameObject.GetComponent<InteractableItem>();
        intItem.RemoveFocus();
    }

    private void Highlight(GameObject gameObject)
    {
        InteractableItem intItem = gameObject.GetComponent<InteractableItem>();
        intItem.SetFocus();
    }

}
