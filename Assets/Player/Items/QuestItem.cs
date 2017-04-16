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

        public QuestItem(Sprite image, string description)
        {
            this.image = image;
            this.description = description;
        }
    }
}
