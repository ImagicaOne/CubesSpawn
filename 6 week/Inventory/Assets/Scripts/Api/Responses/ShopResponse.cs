using System.Collections.Generic;
using InventorySystem.Item;

namespace Api.Responses
{
    public class ShopResponse
    {
        public Item Item { get; set; }
        public List<Item> GameItems { get; set; }
    }
}