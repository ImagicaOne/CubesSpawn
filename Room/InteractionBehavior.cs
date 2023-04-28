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

    void FixedUpdate()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        // _doorsLayer processing
        if (Physics.Raycast(ray, out _hitInfo, 3f, _doorsLayer))
        {           
            var doorObject = _hitInfo.collider.gameObject;
            Debug.Log($"This is door {doorObject}");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Door door = doorObject.GetComponent<Door>();
                door.SwitchDoorState();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowAll();
            }
        }

        // _InteractiveItemsLayer processing
        else if (Physics.Raycast(ray, out _hitInfo, 2f, _InteractiveItemsLayer))
        {            
            // if target goes from one object to another, unhighlight previous object
            if (_currentGameObject!= null && _currentGameObject != _hitInfo.collider.gameObject)
            {
                ClearHighlighting(_currentGameObject);
            }

            _currentGameObject = _hitInfo.collider.gameObject;
            Debug.Log($"This is Interactive object {_currentGameObject}");
            Highlight(_currentGameObject);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (transform.Find("InventoryHolder").childCount > 0)
                {
                    ThrowAll();
                }

                LiftItem();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowAll();
            }
        }
        else
        {
            if (_currentGameObject != null) //&& _currentGameObject.GetComponent<InteractableItem>() != null)
            {
                ClearHighlighting(_currentGameObject);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowAll();
            }
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
            child.transform.SetParent(transform.Find("InteractableInventory"));
            child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            child.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
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
