using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NPC

{
    public abstract class NPC
    {
        protected int mood;
        protected List<string> items;
        protected System.Random random = new System.Random();

        /// <summary>
        /// Add to NPCs mood level. Mood is limited to 0-100.
        /// </summary>
        /// <param name="mood"></param>
        public void changeMood(int mood)
        {
            this.mood += mood;
            if (this.mood > 100)
            {            
                this.mood = 100;
            }
            if (this.mood < 0)
            {
                this.mood = 0;
            }
        }
    }
}