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
    public class CollisionGuitarGod : Collision
    {
        private GameObject guitargod;
        private GuitarGod gh;

        override protected void Start()
        {
            base.Initialization();
            guitargod = GameObject.Find("GuitarGod");
            gh = FindObjectOfType<GuitarGod>();
            collisionText = "Play guitar";
            guitargod.SetActive(false);
        }

        override public void Interaction()
        {
            guitargod.SetActive(true);
            gh.NewGame();
            playerMovement.Stop = true;
        }

        public void ShowInteraction()
        {
            iManager.ShowBox(collisionText, iManager.buttonInteraction);
        }
    }
}