using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Player;
using Dialogue;
using Game;
using Interface;

namespace NPC
{
    public class CollisionDoor : Collision
    {
        void Start()
        {
            this.controller = FindObjectOfType<GameController>();
            this.iManager = FindObjectOfType<InterfaceManager>();
            this.events = controller.GetEvents();
            this.player = events.GetPlayer();
            collisionText = "Enter";
        }

        override public void Interaction()
        {
            Debug.Log("Door click");
        }
    }
}
