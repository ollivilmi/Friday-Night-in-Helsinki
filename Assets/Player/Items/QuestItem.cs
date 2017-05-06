using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class QuestItem
    {
        public string description { get; set; }
        public Sprite image { get; set; }
        public string name { get; set; }
        /// <summary>
        /// Quest items are not usable, they are there just to tell
        /// about what they are, where they are from etc.
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="image">The image you see in the inventory.</param>
        /// <param name="description">Description text in the inventory for the item.</param>
        public QuestItem(string name, Sprite image, string description)
        {
            this.name = name;
            this.image = image;
            this.description = description;
        }
    }
}
