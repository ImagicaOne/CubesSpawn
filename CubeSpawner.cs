using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _cubePrefab;

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var newCube = Instantiate(_cubePrefab);
            newCube.transform.position = new Vector3(Random.Range(-10, 10), 10, -10);
        }
    }
}
