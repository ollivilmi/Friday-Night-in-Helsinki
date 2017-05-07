
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using Dialogue;


namespace Player
{
	public class Jarno : Player  
	{
        public Jarno()
        {
            this.hour = 17;
            this.minute = 0;
            this.special = "Could you spare some change?";
            this.name = "Jarno";
            this.speed = 16f;
            this.likability = 100;
            this.money = 50;
            this.drunkLevel = 0;
            this.funLevel = 20;
            this.height = -8.5f;
            this.playerAnimator.SetBool("isJarno", true);
            items[0].amount = 2;
            items[1].amount = 20;
            Sprite guitar = Resources.Load<Sprite>("eguitarSquare");
            itemsQuest.Add(new QuestItem("Guitar", guitar, "Electric guitar. You need an amplifier to play it."));
			this.playerSprite = Resources.Load<Sprite> ("Jarno");
			this.backStory = "The Bohemian\n\nOccupation: Musician / Unemployed\n\nJarno goes by his charisma and the money from The Social Insurance Institution to get by in life.";
			character.sprite = playerSprite;
        }

		override public string Think()
		{
			return "I am Jarno.";
		}
        /// <summary>
        /// Jarno's Special is begging for money, and the result is randomized.
        /// </summary>
        /// <returns></returns>
		override public string Special()
		{
            switch (random.Next(0, 4))
            {
                case 0:
                    useMoney(10);
                    haveFun(3);
                    return "Well, you don't look like someone who would use it on alcohol so I guess I'll make an exception.";
                case 1:
                    useMoney(10);
                    haveFun(3);
                    return "I usually don't give anything to beggars... But you're pretty cool.";
                case 2:
                    haveFun(-2);
                    return "No. Why would I give money to you and not African children?";
                case 3:
                    haveFun(-6);
                    return "Begging should be made illegal. People like you make me sick.";
                default:
                    return "";
            }
        }
        /// <summary>
        /// Randomizes NPCs reply in case you ask again.
        /// </summary>
        /// <returns></returns>
		override public string SpecialUsed()
		{
            string[] specialAnswer = new string[] { "What? You're asking me again?", "Please, you asked me already.", "Go bother someone else." };
            return specialAnswer[random.Next(0, specialAnswer.Length)];
		}
			
	
        override public void StoryPetri()
        {
            ReadStoryFile("StoryPetri_Jarno");
        }

        override public void StoryMatti()
        {
            ReadStoryFile("StoryMatti_Jarno");
        }
    }
}