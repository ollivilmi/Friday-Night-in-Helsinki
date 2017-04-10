using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;

namespace NPC {
    public class CollisionBarNPC : CollisionNPC {

        private NPCBar npc;

        void Start()
        {
            controller = FindObjectOfType<GameController>();
            dManager = FindObjectOfType<DialogueManager>();
            events = controller.GetEvents();
            player = events.GetPlayer();
            npc = new NPCBar();
            dHolder = new DialogueHolderBar(player, dManager, npc);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dHolder.moodChange != 0)
            {
                npc.changeMood(dHolder.ReturnMoodChange());
            }
        }
    }
}
