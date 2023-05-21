using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<Vector3> _positions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _positions.Add(_lineRenderer.GetPosition(0));
    }

    public void DrawLine()
    {      
            StartCoroutine(Draw());       
    }

    public List<Vector3> GetRoute()
    {
        return _positions;
    }

    private IEnumerator Draw()
    {
        var position = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        position.z = 0;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        _positions.Add(position);
        yield return null;  
    }

}
