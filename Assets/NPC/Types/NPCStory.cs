using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NPCStory : NPC
    {
        private int x;
        public string Functionality { get; set; }

        public NPCStory()
        {
            items = new List<string>() { "Quest item" };
            x = items.Count - 1;
        }
        /// <summary>
        /// Returns an item when called if the NPC has an item to give.
        /// </summary>
        /// <param name="item"></param>
        public void ReturnItems(out string item)
        {
            if (items.Count > 0)
            {
                item = items[x];
                items.Remove(items[x]);
                x--;
            }
            else item = "";
        }
           
    }
}