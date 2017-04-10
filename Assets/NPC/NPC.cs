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
        protected GameObject sprite;

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

        public int getMood()
        {
            return this.mood;
        }

        public abstract GameObject GetObject();
    }
}