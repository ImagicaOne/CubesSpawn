using System.Collections;
using System.Collections.Generic;
using Api;
using UnityEngine;
using InventorySystem.Item;

public class ShopView : MonoBehaviour
{
    private List<Item> _items  = new();
    
    public void ShowAllItems()
    {
        Debug.Log("-----Shop items-----");
        foreach (var item in _items)
        {
            Debug.Log(item.name + " price: " + item.price);
        }
        Debug.Log("----------");
    }

    public void ShowItemsOfType(ItemType itemType)
    {
        foreach (var item in _items)
        {
            if (item.itemType == itemType)
            {
                Debug.Log(item.name + " " + item.price);
            }
        }
    }

    public void BuyItem(int id)
    {
        WebApi.Instance.ShopAPI.BuyItem(id, OnSuccessBuy, OnError);
    }

    private void Start()
    {
        WebApi.Instance.ShopAPI.GetAllItems(OnSuccessGet, OnError);
    }

    private void OnSuccessGet(List<Item> userItems)
    {
        _items = userItems;
        ShowAllItems();
    }
    
    private void OnSuccessBuy(Item item)
    {
        Debug.Log(item.name + " is bought!");
    }

    private void OnError(string errorMessage)
    {
        Debug.LogError(errorMessage);
    }
}
