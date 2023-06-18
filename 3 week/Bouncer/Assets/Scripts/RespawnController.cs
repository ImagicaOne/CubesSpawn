using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Renderer>().transform.position = new Vector3(0, 1f, 0);
    }
}
