using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player
{
    public abstract class Player
    {
        public int drunkLevel { get; set; }
        protected int likability, funLevel;
        public float speed { get; set; }
        public double money { get; set; }
        public List<string> items { get; set; }
        public string name { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public string[] story { get; set; }
        public string[] reply { get; set; }
        public string[] answer { get; set; }
        public string special { get; set; }


        public Player()
        {
            this.items = new List<string>();
        }

        public string UpdateStats()
        {
            return "Drunk: " + drunkLevel + " Money: " + money +
                " Likability: " + likability + " Fun: " +getfunLevel() +" Items: " +GetItems();
        }

        public void haveFun(int fun)
        {
            funLevel += fun + (int)drunkFun();      //Have fun increases fun level and takes drunkLevel into
            if (funLevel > 50)
            {                   //consideration as well
                funLevel = 50;
            }
            else if (funLevel < -50)
            {
                funLevel = -50;
            }
        }

        public void drink(int amount)
        {
            drunkLevel += amount;
            if (drunkLevel > 100)
            {  //Drink is used to set your drunkLevel 0-100
                drunkLevel = 100;
            }
            if (drunkLevel < 0)
            {
                drunkLevel = 0;
            }
        }


        public int getfunLevel()
        {
            return funLevel;
        }

        public double drunkFun()
        {
            return 0.1 * (double)drunkLevel; //A simple math equation used in fun calculation
        }

        public bool useMoney(double sum)
        {                                               //Used to spend and gain money
            if ((money + sum) >= 0)
            {                                            //When sum is negative spend money and vice versa 
                money += sum;                            //If sum is more than you can spend, return false
                return true;
            }
            else
                return false;
        }

        public int getLikability()
        {
            return likability - (drunkLevel / 3); //Return likability, which is affected by drunkLevel
        }

        public void AddItem(string item)
        {
            items.Add(item);
        }
        public string GetItems()
        {
            string AllItems = "";
            foreach (string item in items)
            {
                AllItems += item+" ";
            }
            return AllItems;
        }
        public abstract string Think();
        public abstract void SetStoryHAY();
        public abstract void SetStoryWAYF();
        public abstract string Special();
        public abstract string SpecialUsed();
    }
}