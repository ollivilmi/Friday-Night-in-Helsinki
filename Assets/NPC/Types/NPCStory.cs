using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;
using Player;
using Game;

namespace NPC
{
    public class NPCStory : NPC
    {
        private int itemIndex;
        private Sprite phone;
        public string Functionality { get; set; }
        protected Story story;
        /// <summary>
        /// Creates a story NPC
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npcname"></param>
        public NPCStory(Player.Player player, string npcname, GameEvents events)
        {
            this.player = player;
            this.events = events;
            phone = Resources.Load<Sprite>("phone");
            itemsQuest = new List<QuestItem>();
            switch (npcname)
            {
                case "Petri":
                    story = new Story(player, this, "How are you?", events);
                    break;
                case "Matti":
                    story = new Story(player, this, "Where are you from?", events);
                    itemsQuest.Add(new QuestItem(phone, "Careful! This is almost antique."));
                    itemIndex = itemsQuest.Count - 1;
                    break;
                default:
                    story = new Story(player, this, "Where are you from?", events);
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
                player.itemsQuest.Add(itemsQuest[itemIndex]);
                itemsQuest.Remove(itemsQuest[itemIndex]);
                itemIndex--;
            }
        }
           
    }
}