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
        private NPCBouncer npc;

        void Start()
        {
            controller = FindObjectOfType<GameController>();
            iManager = FindObjectOfType<InterfaceManager>();
            events = controller.GetEvents();
            player = events.GetPlayer();
            npc = new NPCBouncer();
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
            dHolder.StartDialogue();
        }
    }
}
