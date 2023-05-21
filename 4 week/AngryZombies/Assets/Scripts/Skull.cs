using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField]
    private GameObject _centerOfArea;

    private Rigidbody2D _rg;

    private float _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rg = GetComponent<Rigidbody2D>();
        _rg.isKinematic = true;
    }

    public void PullSkull()
    {
        _rg.isKinematic = true;

        Vector2 _mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        var direction = (_mousePosition - new Vector2(_centerOfArea.transform.position.x, _centerOfArea.transform.position.y)).normalized;
        transform.position = _mousePosition - direction / 5;    //magic number    
        Vector2 rotatedVectorToTarget = Quaternion.Euler(0, 0, 270) * direction;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100);
    }

    public void ThrowSkull()
    {
        var direction = new Vector2(_centerOfArea.transform.position.x, _centerOfArea.transform.position.y) - new Vector2(transform.position.x, transform.position.y);      
        var initialSpeed = Calculator.CalculateInitialSpeed(direction);
        _rg.isKinematic = false;
        // _rg.AddForce(direction.normalized * initialSpeed, ForceMode2D.Impulse);
        _rg.AddForce(direction * 8, ForceMode2D.Impulse);
    }
}
