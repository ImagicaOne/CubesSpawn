using UnityEngine;

public class ObjectsInteractionTask2 : MonoBehaviour
{
    [SerializeField]
    private GameObject lamp;

    [SerializeField]
    private Transform lampRoot;

    // TODO: В методе Awake создайте на сцене в точке LampRoot одну из наборов ламп (из папки Prefabs/Lamps)
    private void Awake()
    {           
        lamp.transform.position = lampRoot.position;
        Instantiate(lamp);
    }
}