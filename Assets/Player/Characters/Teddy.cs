
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
        Sprite phone;
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
            phone = Resources.Load<Sprite>("iphone");
            itemsQuest.Add(new QuestItem(phone, "Your mobile phone. It's turned off today."));
        }

        override public string Think()
        {
            return "I am Teddy.";
        }

        override public string Special()
        {
            useMoney(-10);
            return "What? Thanks I guess... You're still an asshole.";
        }
        override public string SpecialUsed()
        {
            return "Are you trying to humiliate me? I don't want your money.";
        }

        override public void StoryPetri()
        {
            story = new string[] { "Well, I lost my job, wife and kids, but at least I have booze I guess.",
                "I was stuck in a dead end job that I absolutely could care less about and I had had enough. Turns out the only reason I was still married was money.",
                "Could you pass me a cigarette?",
                "I'm just contemplating to end it all... I still have money, but at this rate not for long.",
                "Fuck off, I've already contributed more than you have as a father to society.",
                "I could bash your head to pieces in 5 seconds. Do you want that?",
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
                "I know, I've been one myself.",
                "Sure thing. See you around."
            };
            answer = new string[] { "Continue", "Continue", "Continue", "Continue", "Continue", "Quit" };
        }
    }
}