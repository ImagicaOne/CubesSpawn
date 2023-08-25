using Hero;
using UnityEngine;

public class HeroPlace : MonoBehaviour
{
    void Awake()
    {
        var heroManager = FindObjectOfType<HeroesManager>();
        heroManager.SetHeroToStartPosition(transform);
    }
    
}
