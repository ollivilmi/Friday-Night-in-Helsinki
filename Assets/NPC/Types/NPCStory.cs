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
        protected Story story;

        public NPCStory(Player.Player player, string npcname)
        {
            this.player = player;
            phone = Resources.Load<Sprite>("phone");
            itemsQuest = new List<QuestItem>() { new QuestItem(phone, "Careful! This is almost antique.") };
            i = itemsQuest.Count - 1;
            switch (npcname)
            {
                case "Petri":
                    story = new StoryHAY(player, this);
                    break;
                case "Matti":
                    story = new StoryWAYF(player, this);
                    break;
                default:
                    story = new StoryWAYF(player, this);
                    break;
            }
        }

        public Story GetStory()
        {
            return this.story;
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