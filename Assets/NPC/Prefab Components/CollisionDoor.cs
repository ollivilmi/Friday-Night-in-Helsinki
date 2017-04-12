﻿using System;
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
		Door doorway;

        void Start()
        {
            this.controller = FindObjectOfType<GameController>();
            this.iManager = FindObjectOfType<InterfaceManager>();
			this.doorway = FindObjectOfType<Door> ();
            this.events = controller.GetEvents();
            this.player = events.GetPlayer();
			this.objectName = gameObject.transform.name;
            collisionText = "Enter";
        }

        override public void Interaction()
        {
			doorway.Enter (this);
        }
    }
}
