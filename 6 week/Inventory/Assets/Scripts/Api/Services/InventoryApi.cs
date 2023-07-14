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
    public class InventoryApi
    {
        public void GetAllItems(Action<Dictionary<Item, int>> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_USER_ITEMS_URL;

            WebApi.Instance.StartCoroutine(SendGetAllItemsRequest(url, onSuccess, onError));
        }
        
        public void DeleteItem(int id, Action onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.DELETE_ITEM_URL + id;
            
            WebApi.Instance.StartCoroutine(SendDeleteItemRequest(url, onSuccess, onError));
        }
        
        private IEnumerator SendGetAllItemsRequest(string url,
            Action<Dictionary<Item, int>> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");
            
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
            var response = JsonConvert.DeserializeObject<InventoryResponse>(jsonResponse);
            onSuccess?.Invoke(response.UserItems);
        }
        
        private IEnumerator SendDeleteItemRequest(string url,
            Action onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Delete(url);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {WebApi.Instance.JwtToken}");
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke(webRequest.error);
                yield break;
            }
            
            onSuccess?.Invoke();
        }
    }
}