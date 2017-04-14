using System;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NPCBar : NPC
    {
        private int x;

        public NPCBar()
        {
            mood = random.Next(0, 101);
            items = new List<string>() { "Beer", "Tobacco" };
            x = items.Count - 1;
        }
        /// <summary>
        /// Checks mood. If 100 and the NPC has items, return item.
        /// </summary>
        /// <param name="item"></param>
        public string ReturnItems(out string item)
        {
            item = "";
            if (mood == 100)
            {
                if (items.Count > 0)
                {
                    item = items[x];
                    string temp = "You received " + items[x]+".";
                    items.Remove(items[x]); 
                    x--;
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