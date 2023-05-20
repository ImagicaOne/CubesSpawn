using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void PullLine()
    {
        Vector2 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        _lineRenderer.SetPosition(1, mousePosition);
    }

    public void ResetLine()
    {
        _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(0));
    }
}
