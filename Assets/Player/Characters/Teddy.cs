
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
			this.playerSprite = Resources.Load<Sprite> ("Character3");
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
                "Thanks, but I don't think should take more money from you...", "Uhh... Do I look that poor to you?",
                "Do you think money will make me like you?", "Did you know you can't buy friends?" };
            return randomAnswer[random.Next(0, randomAnswer.Length)];
        }

        override public void StoryPetri()
        {
            story = new string[] { "Well, I lost my job, wife and kids, but at least I have booze I guess.",
                "I was stuck in a dead end job that I absolutely could care less about and I had had enough. Turns out the only reason I was still married was money.",
                "Could you pass me a cigarette?",
                "I'm just contemplating to end it all... I still have money, but at this rate not for long.",
                "Fuck off, I've already contributed more than you have to society. I'm a father of 3 children.",
                "I could bash your head to pieces in 5 seconds. Would you like that?",
                "You know what? What ever. I'm done with you." };
            reply = new string[] { "I like your attitude.",
                "Well, money really is the only thing that matters. Everyone needs to contribute to society.",
                "Give tobacco.",
                "Well, maybe you should. I don't pay taxes to feed people like you.",
                "What an achievement. It must have been exceptionally difficult to have sex without protection.",
                "Ahh, the usual cave man reaction that every civilized member of the society has.",
                "But why? Weren't we just getting along?" };
            answer = new string[] { "Continue", "Continue", "Give tobacco", "Continue", "Continue", "Continue", "Quit" };
        }

        override public void StoryMatti()
        {
            story = new string[] {
            "Ehh, me?",
            "You're right. I was born in Kuopio but I moved here because of my job.",
            "Do you happen to work for StoryCompanyPlaceholder?",
            "I'm Matti. I don't think we've had the pleasure of talking before, but we've probably seen each other.",
            "I've been training new phone salesmen. It's inredibly stressful.",
            "My friend Tommi is here as well. If you find him, please give him this." };
            reply = new string[]
            {
                "Yep. You don't seem like a local.",
                "I knew it. I know some people from Kuopio.",
                "Ahh yes, do I know you?",
                "Ahh... What are you up to?",
                "I know, I've been one myself. Are you with someone?",
                "Sure thing. See you around."
            };
            answer = new string[] { "Continue", "Continue", "Continue", "Continue", "Continue", "Quit" };
        }


    }
}