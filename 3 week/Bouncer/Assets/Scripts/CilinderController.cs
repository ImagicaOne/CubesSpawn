using UnityEngine;
using UnityEngine.Events;

public class CilinderController : MonoBehaviour
{
    public UnityEvent<Color> scoreEvent;

    public GameObject Initialize()
    {
        var newCilinder = Instantiate(gameObject);
        newCilinder.transform.position = new Vector3(Random.Range(-8, 8), 1.6825f, Random.Range(-8, 8));
        newCilinder.GetComponent<Renderer>().material.color = ColorProvider.GetRandomColor();
        return newCilinder;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var color = GetComponent<Renderer>().material.color;
        if (color == collision.gameObject.GetComponent<Renderer>().material.color)
        {
            Destroy(gameObject);
            scoreEvent.Invoke(color);
        }
    }
}
