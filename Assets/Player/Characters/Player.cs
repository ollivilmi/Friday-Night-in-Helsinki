﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Game;
using Interface;
using System.Collections;

namespace Player
{
	public abstract class Player 
    {
        public double drunkLevel { get; set; }
        protected int likability, funLevel;
        public float speed { get; set; }
        public double money { get; set; }
        public List<Item> items { get; set; }
        public List<QuestItem> itemsQuest { get; set; }
        public string name { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public string[] story { get; set; }
        public string[] reply { get; set; }
        public string[] answer { get; set; }
        public string special { get; set; }
        protected System.Random random;
        protected char[] delimiter;
		public Sprite playerSprite { get; set; }
		public string backStory { get; set; }
        public bool interacting { get; set; }
		protected SpriteRenderer character;
		public Animator playerAnimator { get; set; }
        public GameEvents events { get; set; }
        public bool[] questAlexander { get; set; }
        public float height { get; set; }

        public Player()
        {
            questAlexander = new bool[] { false, false };
			this.character = GameObject.Find("Player").GetComponent<SpriteRenderer>();
            delimiter = new char[] {'/'};
            random = new System.Random();
            this.items = new List<Item> { new Beer(this, 0), new Tobacco(this, 0)};
            this.itemsQuest = new List<QuestItem>();
			this.playerAnimator = GameObject.Find ("Player").GetComponent<Animator> ();
        }
        /// <summary>
        /// Returns player's stats in a string format.
        /// </summary>
        /// <returns></returns>
        public string UpdateStats()
        {
            return " Money: " + money;
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
        public void drink(double amount)
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
            return 0.1 * drunkLevel;
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
            return (int)(likability - (drunkLevel / 3));
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
        public abstract void StoryPetri();
        public abstract void StoryMatti();
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

        /// <summary>
        /// Filler story, which NPCs use by default unless they have been given something else.
        /// Length is always 2, and it's randomized.
        /// </summary>
        public void StoryFiller()
        {
            string[] fillerStory1 = { "Uhh... Me? Just fine.", "I'm doing just great.", "I'm fine I guess.", "Nothing much, really.", "Are you talking to me?" };
            string[] fillerStory2 = { "Sorry, I'm really busy right now.", "Sorry, I don't have time to talk.", "Sorry, I'm busy.", "Go bother someone else.", "That was a great conversation. I'm glad we're over it." };
            story = new string[]
            {
                fillerStory1[random.Next(0,fillerStory1.Length)],
                fillerStory2[random.Next(0,fillerStory2.Length)]
            };
            string[] fillerReply1 = { "What brings you here?", "What's your name?", "Where are you from?", "Do you work here?", "What do you think about the weather?", "Have I seen you before?" };
            string[] fillerReply2 = { "Oh okay, good bye then.", "Okay, bye.", "Okay.", "I see... Good bye.", "Maybe some other time.", "Good bye." };
            reply = new string[] {
                fillerReply1[random.Next(0,fillerReply1.Length)],
                fillerReply2[random.Next(0,fillerReply2.Length)],
            };
            answer = new string[]
            {
                "Continue", "Quit"
            };
        }

        /// <summary>
        /// Sets the player's Story variables, which are accessed
        /// when creating a story for an NPC. This is used to make it possible
        /// to have multiple different dialogues for different characters on
        /// one NPC.
        /// </summary>
        /// <param name="name">The name of the story: this is seen in the DialogueOptions. Opening line.</param>
        public void SetStory(string name)
        {
            switch (name)
            {
                case "How are you?":
                    StoryPetri();
                    break;
                case "Where are you from?":
                    StoryMatti();
                    break;
                case "Hey, what's up?":
                    ReadStoryFile("StoryTommi");
                    break;
                case "Hippi-Heikki, is that you?":
                    ReadStoryFile("StoryHeikki");
                    break;
                case "Do I know you?":
                    ReadStoryFile("StoryJartsa");
                    break;
                case "Hey, I found him!":
                    ReadStoryFile("StoryHeikki2");
                    break;
                case "Excuse me sir, but I couldn't help but notice you're in the investment business as well.":
                    ReadStoryFile("StoryAlexander");
                    break;
                case "Howdy there, how's it goin'?":
                    ReadStoryFile("StoryJP");
                    break;
                case "Hey there, pretty lady!":
                    ReadStoryFile("StoryLiinu");
                    break;
                case "Great success!":
                    ReadStoryFile("StoryAlexander2");
                    break;
                case "Jump the queue.":
                    ReadStoryFile("StoryKake");
                    break;
                case "Are you alright?":
                    ReadStoryFile("StoryDrunk");
                    break;
                case "Hey, what are you up to?":
                    ReadStoryFile("StorySpaceman");
                    break;
                default:
                    StoryFiller();
                    break;
            }
        }
        /// <summary>
        /// Gets player Backstory for character selection
        /// </summary>
        /// <returns></returns>
		public string GetBackStory(){
			return this.backStory;
		}
        /// <summary>
        /// Gets player sprite for character selection
        /// </summary>
        /// <returns></returns>
		public Sprite GetPlayerSprite()
		{
			return this.playerSprite;
		}

        /// <summary>
        /// Removes quest item from player's inventory that has the name of the parameter.
        /// </summary>
        /// <param name="item">Compared to QuestItem variable "name"</param>
        public void RemoveQuestItem(string item)
        {
            foreach (QuestItem qi in itemsQuest)
            {
                if (qi.name == item)
                {
                    itemsQuest.Remove(qi);
                    break;
                }
            }
        }
        /// <summary>
        /// Reads story file from Resources folder, sets string[] story, reply and answer from it.
        /// </summary>
        /// <param name="textFile">Name of the file to read.</param>
        protected void ReadStoryFile(string textFile)
        {
            TextAsset storyTxt = (TextAsset)Resources.Load(textFile);
            string[] storyTemp = storyTxt.text.Split("\n"[0]);
            story = storyTemp[0].Split(delimiter);
            reply = storyTemp[1].Split(delimiter);
            answer = storyTemp[2].Split(delimiter);
        }
    }
}