using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;

namespace Dialogue
{
    public class StoryHolder
    {
        private string[] dialogueOpener;
        private Random random;
        private Story story;
        public bool finished { get; set; }

        public StoryHolder(Player.Player player, NPCStory npc)
        {
            random = new Random();
            this.dialogueOpener = new string[] { "Hey.", "Hello.", "Greetings.", "Yes?", "Sup." };
        }

        public void SetStory(Story story)
        {
            this.story = story;
        }

        /// <summary>
        /// Get a random opening line for dialogue option menu which is just a variation of "Hey".
        /// </summary>
        /// <returns></returns>
        public string GetDialogueOpener()
        {
            return dialogueOpener[random.Next(0,dialogueOpener.Length-1)];
        }

        /// <summary>
        /// Use level to access a layer of the story. Return npc dialogue, player's dialogue and what clicking it does.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="answerDialogue"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public string GetStoryDialogue(int level, out string answerDialogue, out string answer)
        {
            if (level == GetStoryLength() -1)
            {
                this.finished = true;
            }
            return story.GetDialogue(level, out answerDialogue, out answer);
        }


        /// <summary>
        /// Get the length of the story to know when it ends.
        /// </summary>
        /// <returns></returns>
        public int GetStoryLength()
        {
            return story.GetLength();
        }

        public string GetStoryName()
        {
            return story.name;
        }
    }
}
