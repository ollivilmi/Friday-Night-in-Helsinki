﻿using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace NPC
{
    public class NPCBar : NPC
    {
        private int itemIndex;

        public NPCBar(GameEvents events)
        {
            this.events = events;
            mood = random.Next(0, 101);
            items = new List<string>() { "Beer", "Tobacco" };
            itemIndex = items.Count - 1;
        }
        /// <summary>
        /// Checks mood. If 100 and the NPC has items, return item.
        /// </summary>
        /// <param name="item"></param>
        public string ReturnItems(out string item)
        {
            events.ChangeTime(3);
            item = "";
            if (mood == 100)
            {
                if (items.Count > 0)
                {
                    item = items[itemIndex];
                    string temp = "You received " + items[itemIndex]+".";
                    items.Remove(items[itemIndex]); 
                    itemIndex--;
                    return temp;
                }
                else
                {
                    return "You're the best!";
                }
                    
            }
            else if (mood >= 80 && mood < 100)
            {
                return "I like you a lot.";
            }
            else if (mood >= 60 && mood < 80)
            {
                return "You're cool.";
            }
            else if (mood >= 40 && mood < 60)
            {
                return "You're ok I guess.";
            }
            else if (mood >= 20 && mood < 40)
            {
                return "I don't like you.";
            }
            else if (mood < 20)
            {
                return "I hate you. Leave.";
            }
            return "";
        }

    }
}