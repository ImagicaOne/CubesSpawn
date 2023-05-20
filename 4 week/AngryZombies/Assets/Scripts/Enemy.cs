using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField] 
    private AudioClip _deathSound;

    private Quaternion _initialRotation;

    public UnityEvent _changeZombieCount;

    private void Start()
    {
        _initialRotation = transform.rotation;
        _changeZombieCount.AddListener(FindObjectOfType<UIController>().ReduceObjectsCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TouchSkull(collision.gameObject) || HasCriticalRotation() || FallFast(collision) )
        {
            Die();
            _changeZombieCount.Invoke();
        }
    }

    private void Die()
    {
        // Создаем эффект "взрыв" на месте убитого зомби.
        CreateExplosion();
        // ПРоигрываем звук смерти зомби.
        PlayDeathSound();
        // Разрушаем объект зомби.
        Destroy(gameObject);
    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
    
    private void CreateExplosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    private bool TouchSkull(GameObject gameObject)
    {
        return gameObject.GetComponent<Skull>() is not null;
    }

    private bool FallFast(Collision2D collision)
    {
        return collision.relativeVelocity.magnitude > 10f;
    }

    private bool HasCriticalRotation()
    {
        return Quaternion.Angle(_initialRotation, transform.rotation) > 30;
    }
}