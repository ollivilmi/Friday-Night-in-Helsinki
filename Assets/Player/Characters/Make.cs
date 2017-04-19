
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Make : Player
    {
        private Sprite knife;
        private string[] specialDialogue;

        public Make()
        {
            specialDialogue = new string[]
            {
                "Käytsä usein täällä?", "Jou, mikä meno?",
                "Kuis menee?", "Mikä svengi?"
            };
            this.special = specialDialogue[random.Next(0, specialDialogue.Length)];
            this.hour = 17;
            this.minute = 0;
            this.special = "Käytsä usein täällä?";
            this.name = "Make";
            this.speed = 16f;
            this.likability = 66;
            this.money = 200;
            this.drunkLevel = 30;
            this.funLevel = 0;
            items[1].amount = 20;
            knife = Resources.Load<Sprite>("Chef_Knife");
            itemsQuest.Add(new QuestItem(knife, "In case of emergency."));
			this.playerSprite = Resources.Load<Sprite> ("Character2");
			this.backStory = "MAKE IS A MAN";
        }

        override public string Think()
        {
            return "I am Make.";
        }

        override public string Special()
        {
            this.special = specialDialogue[random.Next(0, specialDialogue.Length)];
            string[] specialAnswer = new string[] { "What? What does that even mean?", "Could you speak Finnish, please?", "...What?",
            "Oh... You were talking to me?", "Dear god. You're so drunk you are incomprehensible.", "Did you have something more than just alcohol?" };
            return specialAnswer[random.Next(0, specialAnswer.Length)];
        }
        override public string SpecialUsed()
        {
            string[] specialAnswer = new string[] { "Stop bothering me with your gibberish.", "I still can't understand you.", "What?",
            "Someday we'll get rid of foreigners like you.", "Go back to your home country.", "Do you need help? Should I call an ambulance?" };
            return specialAnswer[random.Next(0, specialAnswer.Length)];
        }

        override public void StoryPetri()
        {
            story = new string[] { "Well, I lost my job, wife and kids, but at least I have booze I guess.",
                "You beat me to the punch. Yep, seems like the only thing women care about is money.",
                "I'm all out... Could you pass me one?",
                "I'm just contemplating to end it all... I still have money, but at this rate not for long.",
                "I guess you're right, but at the moment I wish I could just bash some skulls in.",
                "Damn, I really didn't expect to get this kind of support from a stranger.",
                "I think I shouldn't be here right now. You're right, take my beer, I shouldn't be drinking right now." };
            reply = new string[] { "Haha, me too man, me too. Did you quit your job?",
                "That's the way the world works. Now before you get any more emotional let's go for a smoke.",
                "Give tobacco.",
                "Wake up man. That's what absolutely everyone says. It's gonna take a week and you'll be fine.",
                "Maybe you should. Perhaps go to the gym?",
                "Neither did I. Why are we wasting time talking and not drinking?",
                "What? I didn't mean that." };
            answer = new string[] { "Continue", "Continue", "Give tobacco", "Continue", "Continue", "Continue", "Quit" };
        }

        override public void StoryMatti()
        {
            story = new string[] {
            "Kuopio, why?",
            "Ahh, I see.",
            };
            reply = new string[]
            {
                "Nevermind. Thought you were a local.",
                "Good bye.",
            };
            answer = new string[] { "Continue", "Quit" };
        }


    }
}