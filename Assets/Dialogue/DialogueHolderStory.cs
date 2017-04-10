using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueHolderStory : DialogueHolder
    {
        private StoryRandom randomStoryDialogue;
        private NPCStory npc;
        private bool specialUsed;

        public DialogueHolderStory(Player.Player player, DialogueManager dManager, NPCStory npc)
        {
            this.player = player;
            this.dManager = dManager;
            dManager.SetHolder(this);
            this.npc = npc;
            this.randomStoryDialogue = new StoryRandom(player, npc);
            this.level = 0;
            this.touching = false;
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
            if (!dManager.dialogueActive)
            {
                InitializeDialogueOptions();
            }
            if (level == 0 && dManager.dialogueActive) //TODO: don't use level, use a bool
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
            
            dManager.ShowBox(npcDialogue, dManager.dBoxNPC, "Continue"); //At the moment only continue and quit, needs some flexibility. Work in progress.
            dManager.ShowBox("Quit", dManager.dBoxAnswer1, "Quit"); 
            dManager.ShowBox(answerDialogue, dManager.dBoxAnswer2, answer);
            level++;
        }

        /// <summary>
        /// Initialize the following: a story dialogue option, Functionality option if one exists,
        /// character special option. Set dialogueActive to true.
        /// </summary>
        private void InitializeDialogueOptions()
        {
            dManager.ShowBox(randomStoryDialogue.GetDialogueOpener(), dManager.dBoxNPC, "Quit");

            if (npc.Functionality != null)
            {
                dManager.ShowBox(npc.Functionality, dManager.dBoxAnswer1, "Functionality");
            }

            dManager.ShowBox(player.special, dManager.dBoxAnswer2, "Special");
            dManager.ShowBox(randomStoryDialogue.GetStoryName(), dManager.dBoxAnswer3, "Story");
            dManager.dialogueActive = true;
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
                    dManager.ShowBox(npc.Functionality, dManager.dBoxNPC, "Quit"); 
                    break;                                                             
                case "Special": //Character specific
                    if (!specialUsed)
                    {
                        dManager.ShowBox(player.Special(), dManager.dBoxNPC, "Quit");
                        specialUsed = true;                      
                    }
                    else if (specialUsed)
                    {
                        dManager.ShowBox(player.SpecialUsed(), dManager.dBoxNPC, "Quit");
                    }
                    break;
                case "Story": //Character specific
                    if (randomStoryDialogue.finished)
                    {
                        dManager.ShowBox("Hey, we already talked.", dManager.dBoxNPC, "Quit"); //After completing the story dialogue, you can't talk through it again.
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
