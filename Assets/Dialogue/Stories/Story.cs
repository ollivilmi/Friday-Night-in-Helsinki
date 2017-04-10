using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;

namespace Dialogue
{
    public abstract class Story
    {
        protected string[] story, reply, answer;
        public string name { get; set; }
        public bool finished { get; set; }
        protected Player.Player player;
        protected NPCStory npc;

        abstract public string GetDialogue(int level, out string answerDialogue, out string answer);
        abstract public int GetLength();
    }
}
