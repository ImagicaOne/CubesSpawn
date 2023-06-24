using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("speed");

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _speed = 3;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _speed = -3;
        }

        if (!Input.anyKey)
        {
            _speed = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            int number = Random.Range(1, 3);
            _animator.SetTrigger("attack" + number);
            Debug.Log("attack" + number);
        }

        _animator.SetFloat(Speed, _speed);
    }
}
