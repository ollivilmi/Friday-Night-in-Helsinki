using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NPC;

namespace Dialogue
{
    public class StoryWAYF : Story
    {

        public StoryWAYF(Player.Player player, NPCStory npc)
        {
            this.name = "Where are you from?"; //The name of the story is displayed when choosing dialogue options.
            this.player = player;
            this.npc = npc;
            this.finished = false;
            this.player.SetStoryWAYF(); //Sets player's story to gain access to the components.
            this.story = player.story;
            this.reply = player.reply;
            this.answer = player.answer;
        }

        override public string GetDialogue(int level, out string answerDialogue, out string answer)
        {
            if (level == 3) //In this story, the player is asked for a cigarette at level 3.
            {
                player.useMoney(-5);
            }
            if (level == story.Length - 1) //These can be used differently (or not at all) in every story.
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
