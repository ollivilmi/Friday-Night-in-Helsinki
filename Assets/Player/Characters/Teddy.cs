
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
            Sprite phone = Resources.Load<Sprite>("iphone");
            itemsQuest.Add(new QuestItem("iPhone", phone, "Your mobile phone. It's turned off today."));
			this.playerSprite = Resources.Load<Sprite> ("Teddy");
			this.backStory = "Teddy has money";
        }

        override public string Think()
        {
            return "I am Teddy.";
        }

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