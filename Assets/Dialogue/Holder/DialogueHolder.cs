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
        protected int dialogueLength, beerCount, tobaccoCount;
        public string selection { get; set; }
        public StoryHolder storyDialogue { get; set; }
        public int moodChange { get; set; }
        protected Player.Player player;
        protected System.Random random;
        protected string active;

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

        public void BuyItem(string item)
        {
            switch (item)
            {
                case "Beer":
                    if (active != "Beer")
                    {
                        active = "Beer";
                        iManager.ShowBox("That would be 5 euroes for 1 beer, thank you.\n\n\n\nClick the icon again to buy.", iManager.dBoxNPC);
                    }
                    else if (player.money >= 5 && active == "Beer")
                    {
                        player.useMoney(-5);
                        player.items[0].amount++;
                        beerCount++;
                        iManager.ShowBox("You have bought " +beerCount+" beers from me.", iManager.dBoxNPC);
                    }
                    break;
                case "Tobacco":
                    if (active != "Tobacco")
                    {
                        active = "Tobacco";
                        iManager.ShowBox("That would be 6 euroes for 5 cigarettes, thank you.\n\n\n\nClick the icon again to buy.", iManager.dBoxNPC);
                    }
                    else if (player.money >= 6 && active == "Tobacco")
                    {
                        player.useMoney(-6);
                        player.items[1].amount += 5;
                        tobaccoCount += 5;
                        iManager.ShowBox("You have bought " + tobaccoCount + " cigarettes from me.", iManager.dBoxNPC);
                    }
                    break;
            }
        }
    }
}
