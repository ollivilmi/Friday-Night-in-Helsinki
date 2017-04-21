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

        override protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                iManager.SetTarget(this);
                iManager.SetHolder(dHolder);
                try
                {
                    iManager.ShowBox(collisionText, iManager.buttonInteraction, "Start");
                }
                catch (System.ArgumentException)
                {
                }
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
