using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float _recoloringInterval;
    [SerializeField]
    private float _recoloringTime;

    private int gridX = 20;
    private int gridY = 20;
    private Vector3 gridOrigin = Vector3.zero;
    private float gridSpacingOffset = 0.3f;

    private List<GameObject> _items = new List<GameObject>();


    void Start()
    {
        StartCoroutine(SpawnGrid());
    }

    private IEnumerator SpawnGrid()
    {
        for (int row = gridY; row > 0; row--)
        {
            for (int column = 0; column < gridX; column++)
            {
                Vector3 pos = new Vector3(column * gridSpacingOffset, row * gridSpacingOffset, 0) + gridOrigin; //(-row) and standart order
                GameObject item = Instantiate(prefab, pos, Quaternion.identity);
                _items.Add(item);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

    public void Recoloring()
    {
        StartCoroutine(CubesColoring());
        //CubesColoring();
    }

    private IEnumerator CubesColoring()
    {
        Color _startColor;
        var _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
        for (int i = 0; i < _items.Count; i++)
        {
            var renderer = _items[i].GetComponent<Renderer>();
            _startColor = renderer.material.color;
            StartCoroutine(CubeColoring(_startColor, _nextColor, renderer));
            yield return new WaitForSeconds(0.02f);         
        }
    }

    private IEnumerator CubeColoring(Color startColor, Color nextColor, Renderer renderer)
    {        
        var _currentTime = 0f;
        while (_currentTime < _recoloringTime)
        {
            var currentColor = Color.Lerp(startColor, nextColor, _currentTime / _recoloringTime);          
            _currentTime +=Time.deltaTime;
            renderer.material.color = currentColor;

            yield return null;
        }
            
        
    }


}
