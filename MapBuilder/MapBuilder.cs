using System;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _field;
    private GameObject _tile;

    //mustnt change
    private float _stopDistance = 1f;
    private float _speed = 100f;
    private float _stepSize = 1f;

    private Color _canBuildColor = Color.gray;
    private Color _warnColor = Color.red;
    private Color _initialColor;

    private List<Collider> _busyPlaces = new List<Collider>();

    private float _collectedSteps = 0f;

    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    public void StartPlacingTile(GameObject tilePrefab)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var _hitInfo))
        {
            var point = _hitInfo.point;
            // put tile to the ground
            point.y = 0;

            // round up tile start position to move by correct steps 
            // Example: _stepSize = 5, point.x = 52 calculation is => point.x = 52 / 5 * 5 = 50
            point.x = (int)point.x / (int)_stepSize * (int)_stepSize;
            point.z = (int)point.z / (int)_stepSize * (int)_stepSize;
            _tile = Instantiate(tilePrefab, _field.transform);
            _tile.transform.position = point;
            _initialColor = _tile.GetComponentInChildren<Renderer>().material.color;
        }       
    }

    public void Update()
    {
        if (_tile != null)
        {        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var _hitInfo))
            {
                if (CanBulid())
                {
                    ChangeColor(_canBuildColor);
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        ChangeColor(_initialColor);
                        _busyPlaces.AddRange(_tile.GetComponentsInChildren<Collider>());
                        _tile = null;
                        return;
                    }
                }
                else
                {
                    ChangeColor(_warnColor);
                }
              
                var target = _hitInfo.point;

                // put tile to the ground
                target.y = 0;           
                         
                if (ShouldMove(target, out var step))
                {
                    // wait, not to move shoothly
                     _collectedSteps += Time.deltaTime * _speed;
                    
                    //lets make a step
                    if (_collectedSteps > _stepSize)
                    {                        
                       _tile.transform.position = _tile.transform.position + step;                            
                       _collectedSteps = 0;
                    }                    
                }              
            }          
        }       
    }

    private bool ShouldMove(Vector3 target, out Vector3 step)
    {
        var distanceX = target.x - _tile.transform.position.x; 
        var distanceZ = target.z - _tile.transform.position.z;

        // what Vector3 should we add to tile.position to make next step correctly
        if (Math.Abs(distanceX) > Math.Abs(distanceZ))
        {
            if (distanceX > 0)
            {
                step = new Vector3(_stepSize, 0, 0);
            }
            else
            {
                step = new Vector3(-_stepSize, 0, 0);
            }
        }
        else
        {
            if (distanceZ > 0)
            {
                step = new Vector3(0, 0, _stepSize);
            }
            else
            {
                step = new Vector3(0, 0, -_stepSize);
            }
        }      

        return Math.Abs(distanceX) > _stopDistance || Math.Abs(distanceZ) > _stopDistance;
    }

    private bool CanBulid()
    {
       bool placeIsBusy = false;

        foreach (var item in _busyPlaces)
        {
            if (item.bounds.Contains(_tile.transform.position))
            {
                placeIsBusy = true;
                break;
            }
        }
        bool outOfBounds = _field.GetComponent<Collider>().bounds.Contains(_tile.transform.position);
        return outOfBounds && !placeIsBusy;
    }

    private void ChangeColor(Color color)
    {
        _tile.GetComponentInChildren<Renderer>().material.color = color;
    }
}