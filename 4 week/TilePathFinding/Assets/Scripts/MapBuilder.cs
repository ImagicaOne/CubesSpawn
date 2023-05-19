using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] 
    private Map _map;
    [SerializeField] 
    private MapIndexProvider _mapIndexProvider;

    [SerializeField]
    private GameObject _simpleTile;
    [SerializeField]
    private GameObject _obstacleTile;

    private Camera _camera;
    private Tile _currentTile;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void StartPlacingTile(GameObject tilePrefab)
    {
        var tileObject = Instantiate(tilePrefab);
        _currentTile = tileObject.GetComponent<Tile>();
        _currentTile.transform.SetParent(_map.transform);

        if (tilePrefab.tag == "Obstacle")
        {
            _currentTile.isObstacle = true;
        }     
    }

    private void Update()
    {
        //var mousePosition = Input.mousePosition;
        //var ray = _camera.ScreenPointToRay(mousePosition);

        //if (Physics.Raycast(ray, out var hitInfo) && _currentTile != null) 
        //{
        //    // Получаем индекс и позицию тайла по позиции курсора
        //    var tileIndex = _mapIndexProvider.GetIndex(hitInfo.point);
        //    var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
        //    _currentTile.transform.localPosition = tilePosition;

        //    // Проверяем, доступно ли место для постройки тайла
        //    var isAvailable = _map.IsCellAvailable(tileIndex);
        //    // Задаем тайлу соответствующий цвет
        //    _currentTile.SetColor(isAvailable);
            
        //    // Если место недоступно для постройки - выходим из метода
        //    if (!isAvailable)
        //    {
        //        return;
        //    }
            
        //    // Если нажата ЛКМ - устанавливаем тайл 
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        _map.SetTile(tileIndex, _currentTile);
        //        _currentTile.ResetColor();
        //        _currentTile = null;
        //    }
        //}
    }

    public void buildMap()
    {
        for (int i = 0; i < _map.Size.x; i++)
        {
            for (int j = 0; j < _map.Size.y; j++)
            {
                //chose prefab
                var number = UnityEngine.Random.Range(0f, 9f);
                if (number < 5)
                {
                    StartPlacingTile(_simpleTile);
                }
                else
                {
                    StartPlacingTile(_obstacleTile);
                }

                var tileIndex = new Vector2Int();
                tileIndex.Set(i, j);             
                var tilePosition = _mapIndexProvider.GetTilePosition(tileIndex);
                _currentTile.transform.localPosition = tilePosition;
                _map.SetTile(tileIndex, _currentTile);
                _currentTile.placeInMap = tileIndex;
                _currentTile.ResetColor();
                _currentTile = null;
                        
            }
        }    
    }
}