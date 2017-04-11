using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;

namespace NPC
{
    public class CollisionStoryNPC : Collision
    {
        private NPCBouncer npc;

        void Start()
        {
            controller = FindObjectOfType<GameController>();
            dManager = FindObjectOfType<DialogueManager>();
            events = controller.GetEvents();
            player = events.GetPlayer();
            npc = new NPCBouncer();
            dHolder = new DialogueHolderStory(player, dManager, npc);
            collisionText = "Talk";
        }
    }
}
