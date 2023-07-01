using UnityEngine;

//on tiles grid
public class TilesController : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;

    private int _count;

    public void Initialize(int size)
    {      
        _count = (int)Mathf.Pow(size, 2);
        SpawnTiles(_count);
    }

    private void SpawnTiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_tilePrefab, gameObject.transform);
        }
    }
}
