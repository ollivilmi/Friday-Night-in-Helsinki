using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;
using Game;
using UnityEngine;


namespace Dialogue
{
    public class Story
    {
        private string[] story, reply, answer;
        public string name { get; set; }
        public bool finished { get; set; }
        private Player.Player player;
        private NPCStory npc;
        private GameEvents events;

        public Story(Player.Player player, NPCStory npc, string storyName, GameEvents events)
        {
            this.player = player;
            this.npc = npc;
            this.name = storyName;
            this.events = events;
            this.finished = false;
            this.player.SetStory(storyName);
            this.story = player.story;
            this.reply = player.reply;
            this.answer = player.answer;
        }

        /// <summary>
        /// Advances story dialogue by accessing the story from the player class.
        /// </summary>
        /// <param name="level">How far into the story</param>
        /// <param name="answerDialogue">Contains player's answer</param>
        /// <param name="answer">What choosing the answer does</param>
        /// <returns></returns>
        public string GetDialogue(int level, out string answerDialogue, out string answer)
        {
            if (level == story.Length - 1)
            {
                StoryEnding(name);
            }
            events.ChangeTime(2);
            answerDialogue = reply[level];
            answer = this.answer[level];
            return story[level];
        }

        public int GetLength()
        {
            return answer.Length;
        }
        /// <summary>
        /// The result of completing a story
        /// </summary>
        /// <param name="name">Opening line of the story dialogue, "How are you?" for example</param>
        private void StoryEnding(string name)
        {
            CollisionNPCStory jartsa, tommi;
            switch (name)
            {
                case "How are you?":
                    if (player.name != "Teddy")
                    {
                        player.items[0].amount += 8;
                    }
                    break;
                case "Where are you from?":
                    if (player.name != "Make")
                    {
                        npc.ReturnItems();
                        tommi = GameObject.Find("NPCTommi(Clone)").GetComponent<CollisionNPCStory>();
                        tommi.SetFunctionality("Is this your phone?");
                    }
                    break;
                case "Hippi-Heikki, is that you?":
                    jartsa = GameObject.Find("NPCJartsa(Clone)").GetComponent<CollisionNPCStory>();
                    jartsa.ChangeStory("Do I know you?");
                    break;
                case "Hey, I found him!":
                    jartsa = GameObject.Find("NPCJartsa(Clone)").GetComponent<CollisionNPCStory>();
                    jartsa.SetFunctionality("I'm ready, let's play!");
                    break;
                case "Excuse me sir, but I couldn't help but notice you're in the investment business as well.":
                    GameObject.Find("NPCJ-P(Clone)").GetComponent<CollisionNPCStory>().ChangeStory("Howdy there, how's it goin'?");
                    GameObject.Find("NPCLiinu(Clone)").GetComponent<CollisionNPCStory>().ChangeStory("Hey there, pretty lady!");
                    break;
                case "Howdy there, how's it goin'?":
                    player.questAlexander[0] = true;
                    if (player.questAlexander[1])
                    {
                        GameObject.Find("NPCAlexander(Clone)").GetComponent<CollisionNPCStory>().ChangeStory("Great success!");
                    }
                    break;
                case "Hey there, pretty lady!":
                    player.questAlexander[1] = true;
                    if (player.questAlexander[1])
                    {
                        GameObject.Find("NPCAlexander(Clone)").GetComponent<CollisionNPCStory>().ChangeStory("Great success!");
                    }
                    break;
                case "Jump the queue.":
                    events.cutscene.BlackOut();
                    break;
                case "Great success!":
                    events.cutscene.Goldmoney();
                    player.haveFun(-20);
                    events.addScore(100);
                    player.useMoney(1000);
                    break;
            }
        }
    }
}
