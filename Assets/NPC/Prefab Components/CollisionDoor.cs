using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Player;
using Dialogue;
using Game;

namespace NPC
{
    public class CollisionDoor : Collision
    {
        void Start()
        {
            this.controller = FindObjectOfType<GameController>();
            this.dManager = FindObjectOfType<DialogueManager>();
            this.events = controller.GetEvents();
            this.player = events.GetPlayer();
            dHolder = new DialogueHolderDoor(player, dManager);
            collisionText = "Enter";
        }


    }
}
