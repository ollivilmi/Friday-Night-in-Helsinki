using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;
using Interface;

namespace NPC {
    public class CollisionNPCBar : Collision {

        private NPCBar npc;

        override protected void Start()
        {
            base.Initialization();
            npc = new NPCBar(events);
            dHolder = new DialogueHolderBar(player, iManager, npc);
            collisionText = "Talk";
        }

        /*override protected void OnTriggerEnter2D(Collider2D col)
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
        }*/

        override protected void OnTriggerStay2D(Collider2D collision)
        {
            base.OnTriggerStay2D(collision);
            if (!player.interacting)
            {
                iManager.SetHolder(dHolder);
            }
            if (dHolder.moodChange != 0)
            {
                npc.changeMood(dHolder.ReturnMoodChange());
            }
        }

        override public void Interaction()
        {
            dHolder.StartDialogue();
            iManager.SetNPCImage(image);
        }
    }
}
