using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;
using Interface;

namespace NPC
{
    public class CollisionNPCStory : Collision
    {
        private NPCStory npc;
        private NPCType npcType;
        private string functionality;

        override protected void Start()
        {
            base.Initialization();
            npcType = FindObjectOfType<NPCType>();
            npc = npcType.GetType(this, events);
            dHolder = new DialogueHolderStory(player, iManager, npc);
            collisionText = "Talk";
        }

        override protected void OnTriggerStay2D(Collider2D col)
        {
            base.OnTriggerStay2D(col);
        }

        public void ChangeStory(string storyName)
        {
            npc.story = new Story(player, npc, storyName, events);
            dHolder = new DialogueHolderStory(player, iManager, npc);
        }
        /// <summary>
        /// Changes DialogueHolder's story to this NPC's story and starts dialogue.
        /// Changes the NPC's image to this NPC's sprite.
        /// </summary>
        override public void Interaction()
        {
            dHolder.storyDialogue.SetStory(npc.GetStory());
            dHolder.StartDialogue();
            iManager.SetNPCImage(image);
        }
        /// <summary>
        /// Gets this NPC's functionality, if it has one.
        /// </summary>
        /// <returns></returns>
        public string GetFunctionality()
        {
            return this.functionality;
        }
        /// <summary>
        /// Changes this NPC's functionality.
        /// </summary>
        /// <param name="func">Used as 3rd option in DialogueOptions.</param>
        public void SetFunctionality(string func)
        {
            this.functionality = func;
            npc.functionality = this.functionality;
        }
    }
}
