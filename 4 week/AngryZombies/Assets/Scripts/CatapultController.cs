using UnityEngine;
using UnityEngine.Events;

public class CatapultController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent pullEvents;

    [SerializeField]
    private UnityEvent resetEvents;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // to prevent moving beyond the area
            // 1) 
            //var hit = Physics2D.Raycast(new Vector2(_position.x, _position.y), Vector2.zero, Mathf.Infinity);
            // if (hit != null && hit.collider != null)

            // 2)
            //if (_center.GetComponent<CircleCollider2D>().bounds.Contains(_position))

            pullEvents.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            resetEvents.Invoke();
        }

    }   
}
