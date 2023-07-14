using System.Collections.Generic;
using Extensions;
using InventorySystem.Item;
using Newtonsoft.Json;

namespace Api.Responses
{
    public class InventoryResponse
    {
        // Используется пользовательский конвертер JSON,
        // поскольку стандартная сериализация/десериализация в json не может обработать словарь 
        [JsonConverter(typeof(DictionaryJsonConverter<Item, int>))]
        public Dictionary<Item, int> UserItems { get; set; }
    }
}