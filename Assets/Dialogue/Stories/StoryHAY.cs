using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NPC;

namespace Dialogue
{
    public class StoryHAY : Story
    {
        /// <summary>
        /// Uses player character to find the "HAY" story
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        public StoryHAY(Player.Player player, NPCStory npc)
        {
            this.name = "How are you?";
            this.player = player;
            this.npc = npc;
            this.finished = false;
            this.player.SetStoryHAY();
            this.story = player.story;
            this.reply = player.reply;
            this.answer = player.answer;
        }

        override public string GetDialogue(int level, out string answerDialogue, out string answer)
        {
            if (level == 3)
            {
                player.useMoney(-5);
            }
            if (level == story.Length-1)
            {
                string item = "";
                npc.ReturnItems(out item);
                player.AddItem(item);
            }

            answerDialogue = reply[level];
            answer = this.answer[level];
            return story[level];
        }

        override public int GetLength()
        {
            return story.Length;
        }
    }
}
