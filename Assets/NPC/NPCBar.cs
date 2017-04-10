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
            items = new List<string>() { "Love letter", "Phone number" };
            x = items.Count - 1;
        }

        public void ReturnItems(out string item)
        {
            if (mood == 100)
            {
                if (items.Count > 0)
                {
                    item = items[x];                //Return first item in list
                    items.Remove(items[x]);             //Remove first item from list
                    x--;
                }
                else
                    item = "";
                return;
            }
            else if (mood >= 80 && mood < 100)
            {
                Debug.Log("80-99");
            }
            else if (mood >= 60 && mood < 80)
            {
                //TODO: NPC comment
                Debug.Log("60-79");
            }
            else if (mood >= 40 && mood < 60)
            {
                //TODO: NPC comment
                Debug.Log("40-59");
            }
            else if (mood >= 20 && mood < 40)
            {
                //TODO: NPC comment
                Debug.Log("20-39");
            }
            else if (mood < 20)
            {
                //TODO: NPC comment
                Debug.Log("1-19");
            }
            item = "";
        }

        override public GameObject GetObject()
        {
            return this.sprite;
        }
    }
}