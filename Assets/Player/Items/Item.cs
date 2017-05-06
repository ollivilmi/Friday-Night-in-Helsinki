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
        /// <summary>
        /// Adds to the amount. Amount means for example how
        /// many beers the player has.
        /// </summary>
        public void Add()
        {
            amount++;
        }
        /// <summary>
        /// Returns the name of the item.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return itemName;
        }

        abstract public bool UseItem();
    }
}
