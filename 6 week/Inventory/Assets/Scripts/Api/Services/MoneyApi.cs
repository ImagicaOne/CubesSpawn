using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Api.Requests;
using Api.Responses;
using InventorySystem.Item;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using User;

namespace Api.Services
{
    public class MoneyApi
    {
        private readonly int amount = 100;
        
        public void AddMoney(Action<int> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.ADD_MONEY_URL;

            var userMoneyRequest = new UserMoneyRequest
            {
                Amount = amount
            };
            
            WebApi.Instance.StartCoroutine(SendAddMoneyRequest(url, userMoneyRequest, onSuccess, onError));
        }
        
        private IEnumerator SendAddMoneyRequest(string url, UserMoneyRequest userMoneyRequest,
            Action<int> onSuccess, Action<string> onError)
        {
            var jsonRequest = JsonConvert.SerializeObject(userMoneyRequest);
            
            var webRequest = UnityWebRequest.PostWwwForm(url, jsonRequest);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");
            
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(jsonRequest));
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                if (webRequest.downloadHandler.data == null)
                {
                    onError?.Invoke(webRequest.error);
                    yield break;
                }

                var message = Encoding.ASCII.GetString(webRequest.downloadHandler.data);
                var errorMessage = $"Request failed. Error: {webRequest.error}. {message}";
                onError?.Invoke(errorMessage);
                yield break;
            }

            var jsonResponse = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<UserMoneyResponse>(jsonResponse);
            onSuccess?.Invoke(response.Money);
        }
    }
}