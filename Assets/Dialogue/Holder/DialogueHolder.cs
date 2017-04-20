using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using Player;
using Game;
using NPC;
using Interface;

namespace Dialogue
{
    abstract public class DialogueHolder
    {

        protected string[] dialogue, answers;
        protected string answer, answerDialogue, npcDialogue;
        protected InterfaceManager iManager;
        public int level { get; set; }
        protected int dialogueLength;
        public string selection { get; set; }
        public StoryHolder storyDialogue { get; set; }
        public int moodChange { get; set; }
        protected Player.Player player;
        protected System.Random random;

        /// <summary>
        /// Starts the dialogue if you click an NPC while on top of it.
        /// </summary>
        public virtual void StartDialogue()
        {
            if (!iManager.dialogueActive)
            {
                DialogueLevel("");
                iManager.SetDialogueActive(true);
            }
        }

        /// <summary>
        /// Starts dialogue functions depending on the level of the conversation.
        /// </summary>
        abstract public void DialogueLevel(string selection);
  
        /// <summary>
        /// Closes dialogue boxes, let's you move again and resets dialogue level to 0.
        /// </summary>
        protected void QuitDialogue()
        {
            iManager.CloseDialogue();
            iManager.SetDialogueActive(false);
            iManager.ShowBox("Talk", iManager.buttonInteraction, "Start");
        }

        /// <summary>
        /// A horrible getter to return an integer and reset mood back to 0 ...What?
        /// </summary>
        /// <returns></returns>
        public int ReturnMoodChange()
        {
            int temp = moodChange;
            moodChange = 0;
            return temp;
        }
    }
}
