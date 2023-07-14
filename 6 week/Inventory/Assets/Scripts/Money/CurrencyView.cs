using Api;
using TMPro;
using UnityEngine;

namespace Money
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _moneyLabel;
        [SerializeField] 
        private TextMeshProUGUI _gemsLabel;

        public void AddMoney()
        {
            WebApi.Instance.MoneyAPI.AddMoney(OnSuccess, OnError);
        }
        
        private void OnSuccess(int money)
        {
            UpdateMoneyView(money);
            Debug.Log("Money added!");
        }

        private void OnError(string errorMessage)
        {
            Debug.LogError(errorMessage);
        }
        
        public void UpdateMoneyView(int value)
        {
            _moneyLabel.text = value.ToString();
        }
        
        public void UpdateGemsView(int value)
        {
            _gemsLabel.text = value.ToString();
        }
    }
}