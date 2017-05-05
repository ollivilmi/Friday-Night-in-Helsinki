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
        public string functionality { get; set; }
        public Story story { get; set; }
        /// <summary>
        /// Checks NPCType for the NPC's name, and sets it's story and items.
        /// By default the type is filler.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npcname"></param>
        public NPCStory(Player.Player player, string npcname, GameEvents events)
        {
            this.player = player;
            this.events = events;
            itemsQuest = new List<QuestItem>();
            switch (npcname)
            {
                case "Petri":
                    story = new Story(player, this, "How are you?", events);
                    break;
                case "Matti":
                    story = new Story(player, this, "Where are you from?", events);
                    Sprite phone = Resources.Load<Sprite>("phone");
                    itemsQuest.Add(new QuestItem("Old phone", phone, "Careful! This is almost antique."));
                    itemIndex = itemsQuest.Count - 1;
                    break;
                case "Tommi":
                    story = new Story(player, this, "Hey, what's up?", events);
                    break;
                case "Heikki":
                    story = new Story(player, this, "Hippi-Heikki, is that you?", events);
                    break;
                case "Alexander":
                    story = new Story(player, this, "Excuse me sir, but I couldn't help but notice you're in the investment business as well.", events);
                    break;
                case "Kake":
                    story = new Story(player, this, "Jump the queue.", events);
                    break;
                case "Drunk":
                    story = new Story(player, this, "Are you alright?", events);
                    break;
                case "Spaceman":
                    story = new Story(player, this, "Hey, what are you up to?", events);
                    break;
                default:
                    string[] filler = { "How's it going?", "How are you doing?", "Hey, how are you?", "How's it hanging?", "What's up?" };
                    story = new Story(player, this, filler[random.Next(0,filler.Length)], events);
                    break;
            }
        }
        /// <summary>
        /// Return the instance of this npc's story dialogue.
        /// </summary>
        /// <returns></returns>
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