using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayOnRemoving()
    {
        _audioSource.Play();
    }
}
