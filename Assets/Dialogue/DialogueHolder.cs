using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using Player;
using Game;
using NPC;

namespace Dialogue
{
    abstract public class DialogueHolder
    {

        protected string[] dialogue, answers;
        protected string answer, answerDialogue, npcDialogue;
        public bool touching { get; set; }
        protected DialogueManager dManager;
        public int level { get; set; }
        protected int dialogueLength;
        public string selection { get; set; }
        public int moodChange { get; set; }
        protected Player.Player player;


        /// <summary>
        /// Starts the dialogue if you click an NPC while on top of it.
        /// </summary>
        public void StartDialogue()
        {
            if (touching && !dManager.dialogueActive)
            {
                DialogueLevel("");
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
            dManager.CloseDialogue();
            dManager.ShowBox("Talk", dManager.dBoxNPC, "Start");
            level = 0;
            dManager.dialogueActive = false;
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
