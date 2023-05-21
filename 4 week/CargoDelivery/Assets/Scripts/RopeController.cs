using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField]
    private LineController _lineController;
    private List<Vector3> _positions = new List<Vector3> ();

    private Coroutine _coroutine;

    private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _positions = _lineController.GetRoute();
    }

    public void Move()
    {
        _coroutine = StartCoroutine(MoveRope());

    }

    public void StopMovement()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator MoveRope()
    {
        for (int i = 0; i < _positions.Count - 1; i++)
        {
            float nextPointDistance = Vector3.Distance(transform.position, _positions[i + 1]);
            yield return StartCoroutine(MoveStep(_positions[i + 1], nextPointDistance));
        }
    }


    private IEnumerator MoveStep(Vector3 nextPoint, float nextPointDistance)
    {       
        var travelDistance = 0f;

        while (travelDistance < nextPointDistance)
        {
            var frameDistance = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, frameDistance);
            travelDistance += frameDistance;

            yield return null;
        }
    }
}
