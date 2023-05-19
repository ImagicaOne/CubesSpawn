using UnityEngine;

public class FollowTheMouseBehavior : MonoBehaviour
{
    void Update()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = position;
    }
}
