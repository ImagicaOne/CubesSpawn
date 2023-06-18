using UnityEngine;

public class SphereController : MonoBehaviour
{
    public void Initialize()
    {
        var sphere = Instantiate(gameObject);
        sphere.transform.position = new Vector3(Random.Range(-8, 8), 0.65f, Random.Range(-8, 8));
        sphere.GetComponent<Renderer>().material.color = ColorProvider.GetRandomColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        transform.position = new Vector3(Random.Range(-8, 8), 0.65f, Random.Range(-8, 8));
        GetComponent<Renderer>().material.color = ColorProvider.GetRandomColor();
    }
}
