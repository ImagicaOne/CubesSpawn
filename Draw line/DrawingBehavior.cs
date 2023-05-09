using System.Collections.Generic;
using UnityEngine;

public class DrawingBehavior : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> positions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
       lineRenderer = GetComponent<LineRenderer>();
       lineRenderer.startColor = Color.blue;
       lineRenderer.endColor = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearScreen();
        }

        if (Input.GetMouseButton(0))
        {
            DrawLine();
        }

    }

    private void DrawLine()
    {
        Vector3 position = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        positions.Add(position);
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }

    private void ClearScreen()
    {
        positions.Clear();
        lineRenderer.positionCount = positions.Count;
    }
}
