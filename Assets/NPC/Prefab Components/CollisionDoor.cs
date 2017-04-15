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
		Door doorway;

        override protected void Start()
        {
            base.Initialization();
            this.doorway = FindObjectOfType<Door>();
            collisionText = "Enter";
        }

        override public void Interaction()
        {
			doorway.Enter (this);
        }
    }
}
