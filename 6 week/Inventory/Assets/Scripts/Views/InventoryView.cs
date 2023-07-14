using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Api;
using UnityEngine;
using InventorySystem.Item;

public class InventoryView : MonoBehaviour
{
    private Dictionary<Item, int> _items  = new();
    
    public void ShowAllItems()
    {
        Debug.Log("-----My inventory-----");
        foreach (var item in _items)
        {
            Debug.Log(item.Key.name + " count: " + item.Value);
        }
        Debug.Log("----------");
    }

    public void ShowItemsOfType(ItemType itemType)
    {
        foreach (var item in _items)
        {
            if (item.Key.itemType == itemType)
            {
                Debug.Log(item.Key + " " + item.Value);
            }
        }
    }

    public void DeleteItem(int id)
    {
        WebApi.Instance.InventoryAPI.DeleteItem(id, OnSuccessDelete, OnError);
    }
    
    private void Start()
    {
        WebApi.Instance.InventoryAPI.GetAllItems(OnSuccessShow, OnError);
    }

    private void OnSuccessShow(Dictionary<Item, int> userItems)
    {
        _items = userItems;
        ShowAllItems();
    }
    
    private void OnSuccessDelete()
    {
        Debug.Log("Item was deleted!");
        WebApi.Instance.InventoryAPI.GetAllItems(OnSuccessShow, OnError);
    }

    private void OnError(string errorMessage)
    {
        Debug.LogError(errorMessage);
    }
}
