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
        private StoryRandom randomStoryDialogue;
        private NPCStory npc;
        private bool specialUsed;

        public DialogueHolderStory(Player.Player player, InterfaceManager iManager, NPCStory npc)
        {
            this.player = player;
            this.iManager = iManager;
            iManager.SetHolder(this);
            this.npc = npc;
            this.randomStoryDialogue = new StoryRandom(player, npc);
            this.level = 0;
            this.specialUsed = false;
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
            if (level == 0 && iManager.dialogueActive) //TODO: don't use level, use a bool
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
            npcDialogue = randomStoryDialogue.GetStoryDialogue(level, out answerDialogue, out answer);
            
            iManager.ShowBox(npcDialogue, iManager.dBoxNPC, "Continue");
            iManager.ShowBox("Quit", iManager.dBoxAnswer1, "Quit"); 
            iManager.ShowBox(answerDialogue, iManager.dBoxAnswer2, answer);
            level++;
        }

        /// <summary>
        /// Initialize the following: a story dialogue option, Functionality option if one exists,
        /// character special option. Set dialogueActive to true.
        /// </summary>
        private void InitializeDialogueOptions()
        {
            iManager.ShowBox(randomStoryDialogue.GetDialogueOpener(), iManager.dBoxNPC, "Quit");

            if (npc.Functionality != null)
            {
                iManager.ShowBox(npc.Functionality, iManager.dBoxAnswer1, "Functionality");
            }

            iManager.ShowBox(player.special, iManager.dBoxAnswer2, "Special");
            iManager.ShowBox(randomStoryDialogue.GetStoryName(), iManager.dBoxAnswer3, "Story");
            iManager.dialogueActive = true;
            dialogueLength = randomStoryDialogue.GetStoryLength();
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
                    if (randomStoryDialogue.finished)
                    {
                        iManager.ShowBox("Hey, we already talked.", iManager.dBoxNPC, "Quit"); //After completing the story dialogue, you can't talk through it again.
                        break;
                    }
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
                    InitializeStoryDialogue();
                    break;
                case "Quit":
                    QuitDialogue();
                    break;
            }
        }
    }
}
