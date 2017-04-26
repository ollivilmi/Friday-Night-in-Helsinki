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
        private BarFight barfight;

        override protected void Start()
        {
            base.Initialization();
            barfight = FindObjectOfType<BarFight>();
            npc = new NPCBar(events, barfight);
            dHolder = new DialogueHolderBar(player, iManager, npc);
            collisionText = "Talk";
        }

        override protected void OnTriggerStay2D(Collider2D collision)
        {
            base.OnTriggerStay2D(collision);
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
