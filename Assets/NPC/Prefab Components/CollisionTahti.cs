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
    public class CollisionTahti : Collision
    {
        private GameObject tahti;

        override protected void Start()
        {
            base.Initialization();
            tahti = GameObject.Find("Tahti");
            collisionText = "Play";
            tahti.SetActive(false);
        }

        override public void Interaction()
        {
            tahti.SetActive(true);
            playerMovement.Stop = true;
        }
        /// <summary>
        /// Sets "Play" button active
        /// </summary>
        public void ShowInteraction()
        {
            iManager.ShowBox(collisionText, iManager.buttonInteraction);
        }
    }
}
