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
        public List<Item> items { get; set; }
        public string name { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public string[] story { get; set; }
        public string[] reply { get; set; }
        public string[] answer { get; set; }
        public string special { get; set; }


        public Player()
        {
            this.items = new List<Item> { new Beer(), new Tobacco()};
        }
        /// <summary>
        /// Returns player's stats in a string format.
        /// </summary>
        /// <returns></returns>
        public string UpdateStats()
        {
            return "Drunk: " + drunkLevel + " Money: " + money +
                " Likability: " + likability + " Fun: " + getfunLevel();
        }
        /// <summary>
        /// Add to fun level in the limits of -50 to 50. Adds a fun bonus for being drunk.
        /// </summary>
        /// <param name="fun"></param>
        public void haveFun(int fun)
        {
            funLevel += fun + (int)drunkFun();
            if (funLevel > 50)
            {
                funLevel = 50;
            }
            else if (funLevel < -50)
            {
                funLevel = -50;
            }
        }
        /// <summary>
        /// Drink alcohol. Amount of 1 equals 10/100 drunk level.
        /// </summary>
        /// <param name="amount"></param>
        public void drink(int amount)
        {
            drunkLevel += amount;
            if (drunkLevel > 100)
            {
                drunkLevel = 100;
            }
            if (drunkLevel < 0)
            {
                drunkLevel = 0;
            }
        }

        /// <summary>
        /// Returns funLevel.
        /// </summary>
        /// <returns></returns>
        public int getfunLevel()
        {
            return funLevel;
        }
        /// <summary>
        /// A simple math equation to calculate fun bonus for being drunk.
        /// </summary>
        /// <returns></returns>
        public double drunkFun()
        {
            return 0.1 * (double)drunkLevel;
        }
        /// <summary>
        /// A general method for manipulating player's money. Gives player money when positive,
        /// and takes away when negative. However, if the player doesn't have enough money to take
        /// from, doesn't take any money and returns false.
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public bool useMoney(double sum)
        {
            if ((money + sum) >= 0)
            {
                money += sum;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Player's likability is subtracted by a third of the player's drunk level.
        /// </summary>
        /// <returns></returns>
        public int getLikability()
        {
            return likability - (drunkLevel / 3);
        }
        /// <summary>
        /// Adds an item to the player's inventory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(string item)
        {
            switch (item)
            {
                case "Beer":
                    items[0].Add();
                    break;
                case "Tobacco":
                    items[1].Add();
                    break;
                case "Quest Item":
                    items[2].Add();
                    break;
            }
        }
        /// <summary>
        /// Returns all the items player has in a string.
        /// </summary>
        /// <returns></returns>
        public string GetItems()
        {
            string AllItems = "";
            foreach (Item item in items)
            {
                AllItems += item.GetName()+" ";
            }
            return AllItems;
        }
        /// <summary>
        /// Returns a string about the player character's thoughts.
        /// </summary>
        /// <returns></returns>
        public abstract string Think();
        /// <summary>
        /// Sets the characters story variables to HAY so that the NPC can
        /// use them.
        /// </summary>
        public abstract void SetStoryHAY();
        /// <summary>
        /// Sets the characters story variables to WAYF so that the NPC can
        /// use them.
        /// </summary>
        public abstract void SetStoryWAYF();
        /// <summary>
        /// Character specific request option to story npcs.
        /// </summary>
        /// <returns></returns>
        public abstract string Special();
        /// <summary>
        /// Returns dialogue that is NPC uses after character special option has been used.
        /// </summary>
        /// <returns></returns>
        public abstract string SpecialUsed();
    }
}