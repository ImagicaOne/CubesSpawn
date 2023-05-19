using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private CubeController2Points _object;

    [SerializeField]
    private Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = _object.transform.position + _offset;
    }
}
