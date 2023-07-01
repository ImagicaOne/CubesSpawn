using UnityEngine;
using UnityEngine.UI;

// On main canvac
public class MainController : MonoBehaviour
{
    [SerializeField]
    private int _size = 3;

    private GridLayoutGroup[] _grids;

    private void Awake()
    {
        _grids = GetComponentsInChildren<GridLayoutGroup>();
        CalculateGridCellSize();
        GetComponentInChildren<TilesController>().Initialize(_size);
        GetComponentInChildren<ItemsController>().Initialize(_size);
    }

    protected void CalculateGridCellSize()
    {
        // Layout has a square size
        // we know count of mini square items
        // calculate the size of mini squares to fit in Layout
        // change cell size
        foreach (var grid in _grids)
        {
            var cellSize = grid.GetComponent<RectTransform>().rect.width / (_size + grid.spacing.x);
            grid.cellSize = new Vector2(cellSize, cellSize);
        }     
    }
}
