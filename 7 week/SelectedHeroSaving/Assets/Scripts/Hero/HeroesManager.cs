using System.Collections.Generic;
using System.Linq;
using Hero.Settings;
using UnityEngine;

namespace Hero
{
    public class HeroesManager : MonoBehaviour
    {
        public static readonly HeroSettings MaxSettings = new()
        {
            Health = 200,
            Attack = 20,
            Defense = 10,
            Speed = 10
        };

        [SerializeField] private HeroController[] _heroPrefabs;
        [SerializeField] private Transform _heroHolder;


        private readonly List<HeroController> _heroControllers = new();

        private void Awake()
        {
            foreach (var heroPrefab in _heroPrefabs)
            {
                InstantiateHero(heroPrefab);
            }
            
            ActivateSelectedHero(_heroPrefabs[0]);
        }

        public void SaveHero()
        {
            DontDestroyOnLoad(this);
        }
        
        public void SetHeroToStartPosition(Transform heroPlace)
        {
            _heroHolder.position = heroPlace.position;
            _heroHolder.rotation = heroPlace.rotation;
        }
        
        public IReadOnlyList<HeroController> GetHeroes()
        {
            return _heroControllers;
        }
        
        public void ActivateSelectedHero(HeroController hero)
        {
            var selectedHeroName = hero.HeroSettings.Name;
            _heroControllers.FirstOrDefault(heroController => 
                heroController.HeroSettings.Name == selectedHeroName)?.gameObject.SetActive(true);
        }

        private void InstantiateHero(HeroController heroPrefab)
        {
            var heroController = Instantiate(heroPrefab, _heroHolder);
            heroController.gameObject.SetActive(false);
            _heroControllers.Add(heroController);
        }
    }
}