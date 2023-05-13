using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField]
    private LineController _lineController;
    List<Vector3> positions = new List<Vector3> ();

    private Coroutine _coroutine;

    // Start is called before the first frame update
    void Start()
    {
        positions = _lineController.GetRoute();
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
        for (int i = 0; i < positions.Count - 1; i++)
        {          
            yield return StartCoroutine(MoveStep(positions[i], positions[i + 1]));
        }
    }


        private IEnumerator MoveStep(Vector3 start, Vector3 end)
        {
            var time = 2f;
            var currentTime = 0f;

            var distance = Vector3.Distance(start, end);
            var travelTime = distance / time;

            while (currentTime < travelTime)
            {
                transform.position = Vector3.Lerp(start, end, currentTime / travelTime);
                currentTime += Time.deltaTime;

                yield return null;
            }
        }

}
