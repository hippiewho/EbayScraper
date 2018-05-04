using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EbayScraper.Models
{
    public class JsonContainer
    {
        private List<Item> _items;

        public JsonContainer()
        {
            _items = new List<Item>();
        }
        public void addItem(string imageurl, string title, string price)
        {
            _items.Add(new Item(imageurl, title, price));
        }

        public List<Item> GetItemList()
        {
            return _items;
        }

        public String ConvertListToJsonString()
        {
            if (!_items.Any()) return "";

            return JsonConvert.SerializeObject(_items);
        }
    }

    public class Item
    {
        public String ImageUrl;
        public String Title;
        public String Price;

        public Item(string imageUrl, string title, string price)
        {
            ImageUrl = imageUrl;
            Title = title;
            Price = price;
        }

    }
}