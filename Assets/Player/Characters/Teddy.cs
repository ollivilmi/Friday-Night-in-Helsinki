
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Teddy : Player
    {
        public Teddy()
        {
            this.hour = 17;
            this.minute = 0;
            this.special = "Bribe";
            this.name = "Teddy";
            this.speed = 16f;
            this.likability = 33;
            this.money = 1000;
            this.drunkLevel = 0;
            this.funLevel = -20;
            this.height = -7.5f;
            this.playerAnimator.SetBool("isTeddy", true);
            Sprite phone = Resources.Load<Sprite>("iphone");
            itemsQuest.Add(new QuestItem("iPhone", phone, "Your mobile phone. It's turned off today."));
			this.playerSprite = Resources.Load<Sprite> ("Teddy");
			this.backStory = "The Capitalist\n\nOccupation: Banker/Investor\n\nTheore 'Teddy' Walrus never wanted to become a banker, but his father forced him. Teddy quickly became addicted to having all this money. His political views gradually shifted to those of the bourgeoisie, as he turned into one of them. Even after transforming into a career banker and a true capitalist, something from his past life still remained.";
        }

        override public string Think()
        {
            return "I am Teddy.";
        }
        /// <summary>
        /// Teddy can use money to bribe strangers. Randomizes the result, sometimes
        /// the NPC doesn't want Teddy's money, sometimes they do and thank him.
        /// </summary>
        /// <returns></returns>
        override public string Special()
        {
            switch (random.Next(0, 4))
            {
                case 0:
                    useMoney(-10);
                    return "What? Thanks I guess... You're still an asshole.";
                case 1:
                    useMoney(-10);
                    haveFun(7);
                    return "Ohh, thank you!";
                case 2:
                    useMoney(-10);
                    haveFun(4);
                    return "Thanks... I guess?";
                case 3:
                    haveFun(-10);
                    return "Do I look like I need your money? Get out of my face.";
                default:
                    return "";
            }
        }
        /// <summary>
        /// Trying to bribe someone twice is annoying to the NPCs, randomizes a reply.
        /// </summary>
        /// <returns></returns>
        override public string SpecialUsed()
        {
            string[] randomAnswer = { "Are you trying to humiliate me? I don't want your money.",
                "Thanks, but I don't think should take money from you...", "Uhh... Do I look that poor to you?",
                "Do you think money will make me like you?", "Did you know you can't buy friends?" };
            return randomAnswer[random.Next(0, randomAnswer.Length)];
        }

        override public void StoryPetri()
        {
            ReadStoryFile("StoryPetri_Teddy");
        }

        override public void StoryMatti()
        {
            ReadStoryFile("StoryMatti_Teddy");
        }


    }
}