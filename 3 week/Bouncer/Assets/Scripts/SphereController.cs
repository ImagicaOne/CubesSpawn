using UnityEngine;

public class SphereController : MonoBehaviour
{
    private ColorProvider _colorProvider;

    private Vector3 _position => new Vector3(Random.Range(-8, 8), 0.65f, Random.Range(-8, 8));

    private Renderer _renderer;

    public void Initialize(ColorProvider colorProvider)
    {
        _colorProvider = colorProvider;
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _colorProvider.GetRandomColor();
        transform.position = _position;       
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Renderer>().material.color = _renderer.material.color;
        transform.position = _position;
        _renderer.material.color = _colorProvider.GetRandomColor();
    }
}
