using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;

namespace Dialogue
{
    public class Story
    {
        private string[] story, reply, answer;
        public string name { get; set; }
        public bool finished { get; set; }
        private Player.Player player;
        private NPCStory npc;

        public Story(Player.Player player, NPCStory npc, string name)
        {
            this.name = name;
            this.player = player;
            this.npc = npc;
            this.finished = false;
            this.player.SetStory(name);
            this.story = player.story;
            this.reply = player.reply;
            this.answer = player.answer;
        }

        public string GetDialogue(int level, out string answerDialogue, out string answer)
        {
            if (level == story.Length - 1)
            {
                StoryEnding(name);
            }

            answerDialogue = reply[level];
            answer = this.answer[level];
            return story[level];
        }

        public int GetLength()
        {
            return story.Length;
        }

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
