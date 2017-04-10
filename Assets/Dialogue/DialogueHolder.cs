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
    public class DialogueHolder
    {

        private string[] dialogue, answers;
        public bool touching { get; set; }
        private DialogueManager dMan;
        private Dialogue randomDialogue;
        private BarNPC npc;
        public int level { get; set; }
        public string selection { get; set; }
        public int moodChange { get; set; }
        private Player.Player player;

        public DialogueHolder(Player.Player player, DialogueManager dMan, BarNPC npc)
        {
            this.player = player;
            this.dMan = dMan;
            this.npc = npc;
            randomDialogue = new Dialogue();
            level = 0;
            touching = false;
            moodChange = 0;
        }

        /// <summary>
        /// Starts the dialogue if you click an NPC while on top of it.
        /// </summary>
        public void StartDialogue()
        {
            if (touching && !dMan.dialogueActive)
            {
                DialogueLevel();
            }
        }

        /// <summary>
        /// Starts dialogue functions depending on the level of the conversation.
        /// </summary>
        public void DialogueLevel()
        {
            switch (level)
            {
                case 1:
                    GetDialogue1();
                    InitializeDialogue();
                    break;
                case 2:
                    GetDialogue2();
                    InitializeDialogue();
                    break;
                case 3:
                    GetDialogue3();
                    InitializeDialogue();
                    break;
                case 4:
                    dMan.CloseDialogue();
                    ReturnMoodChange();
                    string item = "";
                    npc.ReturnItems(out item);
                    player.items.Add(item);
                    level = 0;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Initializes dialogue answer buttons with the given dialogue strings and strings that contain if they correspond to +/n/-
        /// </summary>
        void InitializeDialogue()
        {
            dMan.ShowBox(dialogue[3], dMan.dNPC, "Quit"); //Show NPC dialogue box, option is quit
            for (int i = 0; i < 3; i++)
            {
                dMan.ShowBox(dialogue[i], dMan.answerButtons[i], answers[i]); //Show answer boxes, tell which answers are +/n/-
            }
        }

        /// <summary>
        /// Starts the first level of dialogue
        /// </summary>
        void GetDialogue1()
        {
            dialogue = randomDialogue.startDialogue1(out answers); //Returns dialogue strings, out = which strings are +/n/-  
            dMan.dialogueActive = true;
        }
        /// <summary>
        /// Starts the second level of dialogue by using the given user input string
        /// </summary>
        void GetDialogue2()
        {
            switch (selection)
            {
                case "Positive":
                    moodChange += ((int)(0.4 * (double)player.getLikability()));
                    dialogue = randomDialogue.startDialogue2(selection, out answers);
                    break;
                case "Neutral":
                    dialogue = randomDialogue.startDialogue2(selection, out answers);
                    moodChange += ((int)(0.1 * (double)player.getLikability()));
                    break;
                case "Negative":
                    dialogue = randomDialogue.startDialogue2(selection, out answers);
                    moodChange -= ((int)(0.4 * (double)player.getLikability()));
                    break;
                case "Quit":
                    dMan.CloseDialogue();
                    level = 0;
                    break;
                default:
                    break;
            }
        }
        void GetDialogue3()
        {
            switch (selection)
            {
                case "Positive":
                    dialogue = randomDialogue.startDialogue3(selection, out answers);
                    moodChange += ((int)(0.4 * (double)player.getLikability()));
                    break;
                case "Neutral":
                    dialogue = randomDialogue.startDialogue3(selection, out answers);
                    moodChange += ((int)(0.1 * (double)player.getLikability()));
                    break;
                case "Negative":
                    dialogue = randomDialogue.startDialogue3(selection, out answers);
                    moodChange -= ((int)(0.4 * (double)player.getLikability()));
                    break;
                case "Quit":
                    dMan.CloseDialogue();
                    level = 0;
                    break;
                default:
                    break;
            }
        }

        public int ReturnMoodChange()
        {
            int temp = moodChange;
            moodChange = 0;
            return temp;
        }
    }
}
