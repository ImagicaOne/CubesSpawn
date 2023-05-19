using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerToClick = 0;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;

    private Bridge[] _bridges;
    private Chest[] _chests;
    private LightFlicker[] _fires;

    private float _distanceToBreakBridge = 5f;
    private float _distanceToInteract = 1.5f;
    private float _distanceToFire = 5f;

    private bool superpower = false;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _bridges = FindObjectsOfType<Bridge>();
        _chests = FindObjectsOfType<Chest>();
        _fires = FindObjectsOfType<LightFlicker>(true);
    }

    private void Update()
    {
        // TODO: Получите точку, по которой кликнули мышью и задайте ее вектор в поле _destination.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var _hitInfo, 1000f, _layerToClick))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _destination = _hitInfo.point;

                // Перемещаем персонажа в направлении _destination.
                _navMeshAgent.SetDestination(_destination);
                
            }          
        }

        ProcessBridges();
        ProcessChests();
        ProcessFires();
    }

    private void ProcessBridges()
    {
        foreach (Bridge bridge in _bridges)
        {
            float distance = Vector3.Distance(transform.position, bridge.transform.position);

            if (distance <= _distanceToBreakBridge && !superpower)
            {           
                bridge.Break();
            }
            if (bridge.transform.position == transform.position)
            {

            }
        }
    }

    private void ProcessChests()
    {
        foreach (Chest chest in _chests)
        {
            float distance = Vector3.Distance(transform.position, chest.transform.position);
            if (distance <= _distanceToInteract)
            {
                chest.Open();
            }
        }
    }

    private void ProcessFires()
    {
        foreach (LightFlicker fire in _fires)
        {
            float distance = Vector3.Distance(transform.position, fire.transform.position);

            if (distance <= _distanceToFire && fire.gameObject.active == false)
            {
                fire.gameObject.SetActive(true);
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
        {
            var outline = GetComponent<Outline>();
            outline.OutlineWidth = 2;
            superpower = true;

            Destroy(other.gameObject);
        }
    }
}