
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

			itemsQuest.Add(new QuestItem(guitar, "Your very own guitar."));
			guitar = Resources.Load<Sprite>("guitar");
			this.playerSprite = Resources.Load<Sprite> ("Character1");
			this.backStory = "Jarno plays the guitar, drinks beer occasionaly, and is a nice guy in general";
		}

		void Start(){

		}
		override public string Think()
		{
			return "I am Jarno.";
		}

		override public string Special()
		{
			useMoney(5);
			return "I usually never give anything to beggars, but this time I'll make an exception just for you.";
		}
		override public string SpecialUsed()
		{
			return "Hey, I already gave you some change.";
		}

		override public void SetStoryHAY()
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

		override public void SetStoryWAYF()
		{
			story = new string[] {
				"Uhh, are you talking to me?",
				"Please just leave me alone.",
				"Jumalauta you are insistent. If you give me a beer I'll tell you.",
				"I was born in Kuopio but I moved here because of my job.",
				"Unlike you, I actually pay my taxes and give something back to the society.",
				"Thanks for the beer I guess. I've seen you before playing guitar, if you want you could ask my friend Tommi for a place in his band." };
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

		public String GetBackStory(){
			return this.backStory;
		}

		public Sprite GetPlayerSprite()
		{
			return this.playerSprite;
		}




	}
}