using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace NPC
{
    public class NPCStory : NPC
    {
        private int i;
        private Sprite phone;
        public string Functionality { get; set; }

        public NPCStory(Player.Player player)
        {
            this.player = player;
            phone = Resources.Load<Sprite>("phone");
            itemsQuest = new List<QuestItem>() { new QuestItem(phone, "Perhaps it could use an upgrade.") };
            i = itemsQuest.Count - 1;
        }
        /// <summary>
        /// Returns an item when called if the NPC has an item to give.
        /// </summary>
        /// <param name="item"></param>
        public void ReturnItems()
        {
            if (itemsQuest.Count > 0)
            {
                player.itemsQuest.Add(itemsQuest[i]);
                itemsQuest.Remove(itemsQuest[i]);
                i--;
            }
        }
           
    }
}