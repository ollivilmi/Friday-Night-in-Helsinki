
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Jarno : Player
    {

        public Jarno(string name, float speed, int likability, float money, int drunkLevel, int funLevel) :
            base(name, speed, likability, money, drunkLevel, funLevel)
        {
            hour = 17;
            minute = 0;
            special = "Could you spare some change?";
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
                "Sure thing, it's the least I could do.",
                "Wake up man. You can still turn your life around. You said you were unhappy with your job. What would you like to do?",
                "Well, that's not really my expertise... But I know one guy.",
                "Stop drinking. Give me all your booze. Get in touch with my friend Jarkko in Kontula.",
                "I wish you the best, hopefully you can sort your life out." };
            answer = new string[] { "Continue", "Continue", "Continue", "Continue", "Continue", "Continue", "Continue" };
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
                "I guess beer is the only way to get you to talk.",
                "Doesn't seem like it was a good decision.",
                "I didn't mean to offend. It just seems like you are on the edge.",
                "It's a big city. We'll see if I run into him."
            };
            answer = new string[] { "Continue", "Continue", "Continue", "Continue", "Continue", "Continue" };
        }
    }
}