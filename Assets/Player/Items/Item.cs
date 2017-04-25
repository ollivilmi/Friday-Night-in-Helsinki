using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public abstract class Item
    {
        public float price { get; set; }
        public int amount { get; set; }
        protected string itemName;
        protected Player player;

        public void Add()
        {
            amount++;
        }

        public string GetName()
        {
            return itemName;
        }

        abstract public bool UseItem();
    }
}
