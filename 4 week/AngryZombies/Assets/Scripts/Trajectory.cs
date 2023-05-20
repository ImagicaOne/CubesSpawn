using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    private GameObject _spritePrefab;

    [SerializeField]
    private GameObject _skull;

    [SerializeField]
    private GameObject _centerOfArea;

    private SpriteRenderer[] _sprites = new SpriteRenderer[30]; //magic number 

    // Start is called before the first frame update
    void Start()
    {
        CreateSprites();
    }

    public void ControlPath()
    {
        var direction = new Vector2(_centerOfArea.transform.position.x, _centerOfArea.transform.position.y) - new Vector2(_skull.transform.position.x, _skull.transform.position.y);
        Vector2 initialSpeedVector = Calculator.CalculateInitialSpeedVector(direction);
        float initialSpeed = Calculator.CalculateInitialSpeed(direction);
        var angle = Mathf.Atan2(initialSpeedVector.y, initialSpeedVector.x);

        float time = 0f;
        foreach (var sprite in _sprites)
        {
            time = time + 0.1f;
            float x = initialSpeed * time * Mathf.Cos(angle);
            float y = initialSpeed * time * Mathf.Sin(angle) - 9.81f * time * time / 2;
            sprite.transform.position = new Vector2(_skull.transform.position.x, _skull.transform.position.y) + new Vector2(x, y);
        }
    }  

    public void HidePath()
    {
        foreach (var sprite in _sprites)
        {
            sprite.transform.position = new Vector2(100, 100);
        }
    }

    private void CreateSprites()
    {
        for (int i = 0; i < 30; i++) //magic number   
        {
            var sprite = Instantiate(_spritePrefab);
            sprite.transform.position = new Vector2(100, 100); //out of the view 
            _sprites[i] = sprite.GetComponent<SpriteRenderer>();
        }
    }
}
