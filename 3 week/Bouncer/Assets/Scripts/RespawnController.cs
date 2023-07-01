using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private CubeController _collisionCube;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<CubeController>();

        if (player != null)
        {
            player.MoveToStartPosition();
        }      
    }
}
