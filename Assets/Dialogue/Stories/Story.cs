﻿using System;
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
            return story.Length;
        }
        /// <summary>
        /// The result of completing a story
        /// </summary>
        /// <param name="name">Opening line of the story dialogue, "How are you?" for example</param>
        private void StoryEnding(string name)
        {
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
                    }
                    break;
            }
        }
    }
}
