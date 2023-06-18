using UnityEngine;
using UnityEngine.Events;

public class CubeController : MonoBehaviour
{
    public UnityEvent scoreEvent;
    private float _power = 10;
    public GameObject Initialize()
    {
        var cube = Instantiate(gameObject);
        cube.transform.position = new Vector3(0, 0.7f, 0);
        return cube;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hitInfo;
        if (Physics.Raycast(ray, out _hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().AddForce((_hitInfo.point - transform.position).normalized * _power, ForceMode.Impulse);
                scoreEvent.Invoke();
            }
        }
    }
}
