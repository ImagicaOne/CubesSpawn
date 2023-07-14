using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Api.Responses;
using InventorySystem.Item;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using User;

namespace Api.Services
{
    public class ShopApi
    {
        public void GetAllItems(Action<List<Item>> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.GET_ALL_GAME_ITEMS_URL;

            WebApi.Instance.StartCoroutine(SendGetItemsRequest(url, onSuccess, onError));
        }
        
        public void BuyItem(int id, Action<Item> onSuccess, Action<string> onError)
        {
            var url = Endpoints.API_URL + Endpoints.BUY_ITEM_URL + id;

            WebApi.Instance.StartCoroutine(SendBuyItemRequest(url, onSuccess, onError));
        }
        
        private IEnumerator SendGetItemsRequest(string url,
            Action<List<Item>> onSuccess, Action<string> onError)
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
            var response = JsonConvert.DeserializeObject<ShopResponse>(jsonResponse);
            onSuccess?.Invoke(response.GameItems);
        }
        
        private IEnumerator SendBuyItemRequest(string url,
            Action<Item> onSuccess, Action<string> onError)
        {
            var webRequest = UnityWebRequest.Put(url, string.Empty);
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
            var response = JsonConvert.DeserializeObject<ShopResponse>(jsonResponse);
            onSuccess?.Invoke(response.Item);
        }
    }
}