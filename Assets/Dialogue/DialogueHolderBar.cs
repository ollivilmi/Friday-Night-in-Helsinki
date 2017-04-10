﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueHolderBar : DialogueHolder
    {
        private NPCBar barNPC;
        private DialogueBar randomBarDialogue;

        public DialogueHolderBar(Player.Player player, DialogueManager dManager, NPCBar npc)
        {
            this.player = player;
            this.dManager = dManager;
            dManager.SetHolder(this); //Set this dialogue to be used in the manager
            this.barNPC = npc;
            this.randomBarDialogue = new DialogueBar();
            level = 0;
            touching = false;
            moodChange = 0;
        }

        public override void DialogueLevel(string selection)
        {
            level++;
            this.selection = selection;
            BarDialogue();
        }
        /// <summary>
        /// Switch used to move the next level of dialogue.
        /// </summary>
        private void BarDialogue()
        {
            switch (level)
            {
                case 1:
                    GetBarDialogue1();
                    InitializeBarDialogue();
                    break;
                case 2:
                    GetBarDialogue2();
                    break;
                case 3:
                    GetBarDialogue3();
                    break;
                case 4:
                    QuitDialogue();
                    ReturnMoodChange();
                    string item = "";
                    barNPC.ReturnItems(out item); //Uses ReturnItems to check the mood level to give items if possible
                    player.items.Add(item);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Initializes all of the dialogue answer buttons, their text and what clicking them does.
        /// </summary>
        private void InitializeBarDialogue()
        {
            dManager.ShowBox(dialogue[3], dManager.dBoxNPC, "Quit"); //Show NPC dialogue box, option is quit
            for (int i = 0; i < 3; i++)
            {
                dManager.ShowBox(dialogue[i], dManager.answerButtons[i], answers[i]); //Show answer boxes, tell which answers are +/n/-
            }
        }

        /// <summary>
        /// Starts the first level of dialogue
        /// </summary>
        private void GetBarDialogue1()
        {
            dialogue = randomBarDialogue.startDialogue1(out answers); //Returns dialogue strings, out = which strings are +/n/-  
            dManager.dialogueActive = true;
        }
        /// <summary>
        /// Starts the second level of dialogue by using the response from the player.
        /// </summary>
        private void GetBarDialogue2()
        {
            switch (selection)
            {
                case "Positive":
                    moodChange += ((int)(0.4 * (double)player.getLikability())); //Changes mood by using the player's likability as a multiplier.
                    dialogue = randomBarDialogue.startDialogue2(selection, out answers);
                    InitializeBarDialogue();
                    break;
                case "Neutral":
                    dialogue = randomBarDialogue.startDialogue2(selection, out answers);
                    moodChange += ((int)(0.1 * (double)player.getLikability()));
                    InitializeBarDialogue();
                    break;
                case "Negative":
                    dialogue = randomBarDialogue.startDialogue2(selection, out answers);
                    moodChange -= ((int)(0.4 * (double)player.getLikability()));
                    InitializeBarDialogue();
                    break;
                case "Quit":
                    QuitDialogue();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Starts the third level of dialogue by using the response from the player.
        /// </summary>
        private void GetBarDialogue3()
        {
            switch (selection)
            {
                case "Positive":
                    dialogue = randomBarDialogue.startDialogue3(selection, out answers); //Changes mood by using the player's likability as a multiplier.
                    moodChange += ((int)(0.4 * (double)player.getLikability())); 
                    InitializeBarDialogue();
                    break;
                case "Neutral":
                    dialogue = randomBarDialogue.startDialogue3(selection, out answers);
                    moodChange += ((int)(0.1 * (double)player.getLikability()));
                    InitializeBarDialogue();
                    break;
                case "Negative":
                    dialogue = randomBarDialogue.startDialogue3(selection, out answers);
                    moodChange -= ((int)(0.4 * (double)player.getLikability()));
                    InitializeBarDialogue();
                    break;
                case "Quit":
                    QuitDialogue();
                    break;
                default:
                    break;
            }
        }
    }
}
