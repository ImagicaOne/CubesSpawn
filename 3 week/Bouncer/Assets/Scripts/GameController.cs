using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ColorProvider _colorProvider;

    [SerializeField]
    private CilinderController _cilinder;
    [SerializeField]
    private CubeController _cube;
    [SerializeField]
    private SphereController _sphere;

    [SerializeField]
    private ScoreController _scoreController;

    [SerializeField]
    private ScoreView _scoreView;

    [SerializeField]
    private int _cylindersCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        _scoreController.Initialize(_colorProvider);
        _scoreView.Initialize(_colorProvider);

        for (int i = 0; i < _cylindersCount; i++)
        {
            var newCilinder = Instantiate(_cilinder);
            newCilinder.Initialize(_colorProvider, () => _scoreController.UpdateScore(newCilinder.Renderer.material.color));
             _scoreController.SetInitialScore(newCilinder.Renderer.material.color);
        }

        var newCube = Instantiate(_cube);
        newCube.Initialize(() => _scoreController.UpdateScore(Color.white));

        var newSphere = Instantiate(_sphere);
        newSphere.Initialize(_colorProvider);
    }
}
