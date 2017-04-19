using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;
using UnityEngine;
using UnityEngine.UI;
using Interface;

namespace Dialogue
{
    public class DialogueHolderStory : DialogueHolder
    {
        private NPCStory npc;
        private bool specialUsed, optionsActive;
        private string[] finishDialogue;

        public DialogueHolderStory(Player.Player player, InterfaceManager iManager, NPCStory npc)
        {
            this.random = new System.Random();
            this.player = player;
            this.iManager = iManager;
            iManager.SetHolder(this);
            this.npc = npc;
            this.storyDialogue = new StoryHolder(player, npc);
            this.level = 0;
            this.specialUsed = false;
            this.optionsActive = false;
            this.finishDialogue = new string[] { "Hey, we already talked.", "Yes? We talked already, didn't we?", "You again?", "Oh hello... Again." };
        }

        /// <summary>
        /// Uses the selected answer to continue the dialogue.
        /// </summary>
        /// <param name="selection"></param>
        public override void DialogueLevel(string selection)
        {
            this.selection = selection;
            StoryDialogue();
        }

        /// <summary>
        /// Initializes story dialogue by using story dialogue from the character being played,
        /// character specials, and checks if the NPC has a functionality. (For example buying a ticket)
        /// </summary>
        private void StoryDialogue()
        {
            if (!iManager.dialogueActive)
            {
                InitializeDialogueOptions();
            }
            if (optionsActive) //TODO: don't use level, use a bool
            {
                DialogueOptions();
            }
            else if (level < dialogueLength)
            {
                StoryOptions();
            }
            else if (level == dialogueLength)
            {
                QuitDialogue();
            }
        }

        /// <summary>
        /// Initializes the answer options when engaged in a story dialogue.
        /// </summary>
        private void InitializeStoryDialogue()
        {
            npcDialogue = storyDialogue.GetStoryDialogue(level, out answerDialogue, out answer);
            
            iManager.ShowBox(npcDialogue, iManager.dBoxNPC, "Quit");
            iManager.ShowBox("Quit", iManager.dBoxAnswer1, "Quit"); 
            iManager.ShowBox(answerDialogue, iManager.dBoxAnswer2, answer);
        }

        /// <summary>
        /// Initialize the following: a story dialogue option, Functionality option if one exists,
        /// character special option.
        /// </summary>
        private void InitializeDialogueOptions()
        {
            iManager.ShowBox(storyDialogue.GetDialogueOpener(), iManager.dBoxNPC, "Quit");

            if (npc.Functionality != null)
            {
                iManager.ShowBox(npc.Functionality, iManager.dBoxAnswer1, "Functionality");
            }

            iManager.ShowBox(player.special, iManager.dBoxAnswer2, "Special");
            iManager.ShowBox(storyDialogue.GetStoryName(), iManager.dBoxAnswer3, "Story");
            dialogueLength = storyDialogue.GetStoryLength();
            optionsActive = true;
        }

        /// <summary>
        /// The logic for what happens behind each option.
        /// </summary>
        private void DialogueOptions()
        {
            switch (selection)
            {
                case "Functionality": //NPC Specific
                    iManager.ShowBox(npc.Functionality, iManager.dBoxNPC, "Quit"); 
                    break;                                                             
                case "Special": //Character specific
                    if (!specialUsed)
                    {
                        iManager.ShowBox(player.Special(), iManager.dBoxNPC, "Quit");
                        specialUsed = true;                      
                    }
                    else if (specialUsed)
                    {
                        iManager.ShowBox(player.SpecialUsed(), iManager.dBoxNPC, "Quit");
                    }
                    break;
                case "Story": //Character specific
                    if (storyDialogue.finished)
                    {
                        iManager.ShowBox(finishDialogue[random.Next(0,4)], iManager.dBoxNPC, "Quit"); //After completing the story dialogue, you can't talk through it again.
                        break;
                    }
                    optionsActive = false;
                    InitializeStoryDialogue();
                    break;
                case "Quit":
                    QuitDialogue();
                    break;
            }
        }
        /// <summary>
        /// The logic for what happens behind each story answer option.
        /// </summary>
        private void StoryOptions()
        {
            switch (selection)
            {
                case "Continue":
                    level++;
                    InitializeStoryDialogue();
                    break;
                case "Give beer":
                    if (player.items[0].amount > 0)
                    {
                        player.items[0].amount--;
                        level++;
                        InitializeStoryDialogue();
                    }
                    else iManager.ShowBox("You don't have beer.", iManager.dBoxNPC, "Quit");
                    break;
                case "Give tobacco":
                    if (player.items[1].amount > 0)
                    {
                        player.items[1].amount--;
                        level++;
                        InitializeStoryDialogue();
                    }
                    else iManager.ShowBox("You don't have tobacco.", iManager.dBoxNPC, "Quit");
                    break;
                case "Quit":
                    QuitDialogue();
                    break;
            }
        }
    }
}
