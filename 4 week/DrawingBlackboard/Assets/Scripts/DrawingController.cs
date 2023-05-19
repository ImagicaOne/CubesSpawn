using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawingController : MonoBehaviour
{
    [SerializeField]
    private GameObject _circle;

    [SerializeField]
    private GameObject _line;

    [SerializeField]
    private GameObject _board;

    [SerializeField]
    private LayerMask _layer;

    private LineRenderer _lineRenderer;

    private List<Vector3> positions = new List<Vector3>();

    private bool drawing = false;
    private bool colorSelected = false;
    private int index = 2;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var _hitInfo) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Debug.Log(_hitInfo.collider.gameObject);

            if (Input.GetMouseButton(0))
            {
                if (drawing == false && colorSelected)
                {
                    StartDrawing();
                }
                DrawLine();
            }
            if (Input.GetMouseButtonUp(0) && drawing)
            {
                drawing = false;
            }                       
        }          
    }

    private void DrawLine()
    {
        if (drawing)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //position.z = 0;
            positions.Add(position);
            _lineRenderer.positionCount = positions.Count;
            _lineRenderer.SetPositions(positions.ToArray());
        }
       
    }

    private void StartDrawing()
    {
        positions.Clear();
        var newLine = Instantiate(_line, _board.GetComponent<Transform>());
        newLine.GetComponent<ChangeColorBehavior>().ChangeColor(_circle.GetComponent<Renderer>().material.color);
        _lineRenderer = newLine.GetComponent<LineRenderer>();
        _lineRenderer.sortingOrder = index++; 

        drawing = true;
    }

    public void ChooseColor()
    {
        _circle.GetComponent<ChangeColorBehavior>().ChangeColor(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color);
        _circle.SetActive(true);
        colorSelected = true;
    }

    public void Clear()
    {
        var lines = _board.GetComponentsInChildren<LineRenderer>();
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
    }

}
