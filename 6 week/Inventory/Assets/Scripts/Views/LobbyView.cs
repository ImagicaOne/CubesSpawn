using Hero;
using TMPro;
using UnityEngine;

namespace Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameText;
        [SerializeField] 
        private TextMeshProUGUI _levelText;
        [SerializeField] 
        private TextMeshProUGUI _experienceText;

        public void Initialize(HeroesSettings heroSettings)
        {
            _nameText.text = heroSettings.Name;
            _levelText.text = heroSettings.Level.ToString();
            _experienceText.text = heroSettings.Experience.ToString();
        }
    }
}