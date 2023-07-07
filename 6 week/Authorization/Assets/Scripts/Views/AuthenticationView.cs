using System;
using Api;
using TMPro;
using UnityEngine;
using User;

namespace Views
{
    public class AuthenticationView : MonoBehaviour
    {
        private const string errorTitle = "Error message\r\n\r\n";
        
        public event Action<MonoBehaviour, UserProfile> Authorized;

        [SerializeField] 
        private TMP_InputField _usernameInput;
        [SerializeField] 
        private TMP_InputField _passwordInput;
        [SerializeField] 
        private GameObject _preloader;
        
        [SerializeField] 
        private GameObject _errorPopup;

        public void Register()
        {
            if (!ValidateInput())
            {
                OnError(-1);
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendRegistrationRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        public void LogIn()
        {
            if (!ValidateInput())
            {
                OnError(-1);
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendLoginRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(_usernameInput.text) || string.IsNullOrEmpty(_passwordInput.text))
            {
                return false;
            }
            
            return true;
        }

        private void OnSuccess(UserProfile userProfile)
        {
            _preloader.gameObject.SetActive(false);
            Authorized?.Invoke(this, userProfile);
        }

        private void OnError(int errorCode)
        {
            var message = ErrorMessageProvider.Instance.GetErrorMessage(errorCode);
            ShowErrorPopup(message);
        }

        private void ShowPreloader()
        {
            _preloader.gameObject.SetActive(true);
        }
        
        private void HidePreloader()
        {
            _preloader.gameObject.SetActive(false);
        }
        
        private void ShowErrorPopup(string message)
        {
            HidePreloader();
            var textComponent = _errorPopup.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = errorTitle + message;
            _errorPopup.gameObject.SetActive(true);
        }
    }
}