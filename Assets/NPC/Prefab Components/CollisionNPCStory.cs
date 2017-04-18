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

        override protected void Start()
        {
            base.Initialization();
            npcType = FindObjectOfType<NPCType>();
            npc = npcType.GetType(this, events);
            dHolder = new DialogueHolderStory(player, iManager, npc);
            collisionText = "Talk";
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
