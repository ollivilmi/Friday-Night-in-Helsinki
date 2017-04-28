
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
            this.height = -8.2f;
            items[1].amount = 20;
            Sprite knife = Resources.Load<Sprite>("Chef_Knife");
            itemsQuest.Add(new QuestItem("Knife", knife, "In case of emergency."));
			this.playerSprite = Resources.Load<Sprite> ("Make");
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
            ReadStoryFile("StoryPetri_Make");
        }

        override public void StoryMatti()
        {
            ReadStoryFile("StoryMatti_Make");
        }
    }
}