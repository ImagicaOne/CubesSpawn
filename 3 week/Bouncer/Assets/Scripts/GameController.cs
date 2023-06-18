using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CilinderController _cilinder;
    [SerializeField]
    private CubeController _cube;
    [SerializeField]
    private SphereController _sphere;

    [SerializeField]
    private ScoreController _scoreController;

    private int _cilindersCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _cilindersCount; i++)
        {
            var newCilinder = _cilinder.Initialize();
            newCilinder.GetComponent<CilinderController>().scoreEvent.AddListener((x) => _scoreController.IncreaseScore(newCilinder.GetComponent<Renderer>().material.color));
            _scoreController.SetInitialScore(newCilinder.GetComponent<Renderer>().material.color);
        }

        var newCube = _cube.Initialize();
        newCube.GetComponent<CubeController>().scoreEvent.AddListener(() => _scoreController.IncreaseScore(Color.white));

        _sphere.Initialize();      
    }
}
