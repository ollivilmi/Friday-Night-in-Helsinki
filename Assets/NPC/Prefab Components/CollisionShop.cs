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
    public class CollisionShop : Collision
    {
        private NPCStory npc;
        private string functionality;

        override protected void Start()
        {
            base.Initialization();
            npc = new NPCStory(player, "Shop", events);
            dHolder = new DialogueHolderStory(player, iManager, npc);
            collisionText = "Bartender";
            npc.functionality = "What do you have for sale?";
        }

        protected override void OnTriggerStay2D(Collider2D col)
        {
            base.OnTriggerStay2D(col);
            if (!player.interacting)
            {
                iManager.SetHolder(dHolder);
            }
        }

        override public void Interaction()
        {
            dHolder.storyDialogue.SetStory(npc.GetStory());
            dHolder.StartDialogue();
            iManager.SetNPCImage(image);
        }
    }
}
