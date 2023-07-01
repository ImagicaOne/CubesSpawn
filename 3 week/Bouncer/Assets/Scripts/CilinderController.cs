using System;
using UnityEngine;
using UnityEngine.Events;

public class CilinderController : MonoBehaviour
{
    private ColorProvider _colorProvider;

    public Renderer Renderer;

    private Action _cylinderDied;

    private Vector3 _position => new Vector3(UnityEngine.Random.Range(-8, 8), 1.6825f, UnityEngine.Random.Range(-8, 8));  

    public void Initialize(ColorProvider colorProvider, Action cylinderDied)
    {
        _colorProvider = colorProvider;
        Renderer = GetComponent<Renderer>();
        Renderer.material.color = _colorProvider.GetRandomColor();
        transform.position = _position;
        _cylinderDied += cylinderDied;

    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<CubeController>();
        if (player != null)
        {
            var color = Renderer.material.color;
            if (color == collision.gameObject.GetComponent<Renderer>().material.color)
            {
                Destroy(gameObject);
                _cylinderDied.Invoke();
            }
        }  
    }
}
