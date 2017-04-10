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
                dManager.ShowBox(randomStoryDialogue.GetDialogueOpener(), dManager.dBoxNPC, "Quit");

                if (npc.Functionality != null) //Don't open a chat option for a functionality if the npc doesn't have one
                {
                    dManager.ShowBox(npc.Functionality, dManager.dBoxAnswer1, "Functionality");
                }
                dManager.ShowBox(player.special, dManager.dBoxAnswer2, "Special");
                dManager.ShowBox(randomStoryDialogue.GetStoryName(), dManager.dBoxAnswer3, "Story"); //Randomize a story dialogue option, get the opening line for the story
                                                                                                     //by using GetStoryName();
                dManager.dialogueActive = true; //Set dialogue active to prevent movement and to ignore this (!dManager.dialogueActive)

                dialogueLength = randomStoryDialogue.GetStoryLength(); //Check the length of the dialogue to stop it when the end is reached
            }
            if (level == 0 && dManager.dialogueActive) //TODO: don't use level, use a bool
            {
                switch (selection)
                {
                    case "Functionality":
                            dManager.ShowBox(npc.Functionality, dManager.dBoxNPC, "Quit"); //If the npc had a functionality option you could click, then use it here.
                        break;                                                             //this is still a work in progress.
                    case "Special": //Character specific
                        if (!specialUsed)
                        {
                            dManager.ShowBox(player.Special(), dManager.dBoxNPC, "Quit"); //Every playable character has their own special question, however you can only
                            specialUsed = true;                                           //ask it once.
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
            else if (level < dialogueLength)
            {
                switch (selection) //In case you want to do something else than just continue the dialogue,
                {                  //for example Accept or Decline something but still continue the dialogue. Work in progress!
                    case "Continue":
                        InitializeStoryDialogue();
                        break;
                    case "Quit":
                        QuitDialogue();
                        break;
                }
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
    }
}
