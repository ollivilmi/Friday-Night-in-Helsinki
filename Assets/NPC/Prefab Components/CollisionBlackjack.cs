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
    public class CollisionBlackjack : Collision
    {
        private GameObject blackjack;

        override protected void Start()
        {
            base.Initialization();
            blackjack = GameObject.Find("Blackjack");
            collisionText = "Play";
            blackjack.SetActive(false);
        }

        override public void Interaction()
        {
            blackjack.SetActive(true);
            playerMovement.Stop = true;
        }

        public void ShowInteraction()
        {
            iManager.ShowBox(collisionText, iManager.buttonInteraction);
        }
    }
}