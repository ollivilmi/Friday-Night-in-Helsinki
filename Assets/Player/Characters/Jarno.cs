
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
		private Sprite guitar;
		//public static Sprite playerImage;
		//public string backStory;



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
            items[0].amount = 2;
            items[1].amount = 20;
            guitar = Resources.Load<Sprite>("guitar");
            itemsQuest.Add(new QuestItem(guitar, "Your very own guitar."));
			this.playerSprite = Resources.Load<Sprite> ("Character1");
			this.backStory = "Jarno plays the guitar, drinks beer occasionaly, and is a nice guy in general";
        }

		override public string Think()
		{
			return "I am Jarno.";
		}

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
		override public string SpecialUsed()
		{
            string[] specialAnswer = new string[] { "What? You're asking me again?", "Please, you asked me already.", "Go bother someone else." };
            return specialAnswer[random.Next(0, specialAnswer.Length)];
		}
			
	
        override public void StoryPetri()
        {
            story = new string[] { "Well, I lost my job, wife and kids, but at least I have booze I guess.",
                "I was stuck in a dead end job that I absolutely could care less about and I had had enough. Turns out the only reason I was still married was money.",
                "Could you pass me a cigarette?",
                "I'm just contemplating to end it all... I still have money, but at this rate not for long.",
                "I've always dreamt of owning my own farm and taking care of animals, away from this dirty city.",
                "Really? Are you sure?",
                "I suppose I'll give it a shot, since there is nothing to lose." };
            reply = new string[] { "Huh... That sucks. What happened?",
                "Life can be pretty hard sometimes man... But you could still make it. Is there anything I could do to help?",
                "Give tobacco.",
                "Wake up man. You can still turn your life around. You said you were unhappy with your job. What would you like to do?",
                "Well, that's not really my expertise... But I know one guy.",
                "Stop drinking. Give me all your booze. Get in touch with my friend Jarkko in Kontula.",
                "I wish you the best, hopefully you can sort your life out." };
            answer = new string[] { "Continue", "Continue", "Give tobacco", "Continue", "Continue", "Continue", "Quit" };
        }

        override public void StoryMatti()
        {
            story = new string[] {
            "Uhh, are you talking to me?",
            "Please just leave me alone.",
            "Jumalauta you are insistent. If you give me a beer I'll tell you.",
            "I was born in Kuopio but I moved here because of my job.",
            "Unlike you, I actually pay my taxes and give something back to the society.",
            "Thanks for the beer I guess. I've seen you before playing guitar, if you want you " +
            "could ask my friend Tommi for a place in his band. Oh and take this old phone, it's from his grandma." };
            reply = new string[]
            {
                "Why yes, yes I am.",
                "You don't seem like the rest of the people here.",
                "Give beer.",
                "Doesn't seem like it was a good decision.",
                "I didn't mean to offend. It just seems like you are on the edge.",
                "It's a big city. We'll see if I run into him."
            };
            answer = new string[] { "Continue", "Continue", "Give beer", "Continue", "Continue", "Quit" };
        }
    }
}