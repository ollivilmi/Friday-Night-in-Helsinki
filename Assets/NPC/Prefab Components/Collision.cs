﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;
using Interface;

namespace NPC
{
    abstract public class Collision : MonoBehaviour
    {
        protected DialogueHolder dHolder;
        protected InterfaceManager iManager;
        protected Player.Player player;
        protected Movement playerMovement;
        protected GameController controller;
        protected GameEvents events;
        protected string collisionText, objectName;
        protected Sprite image;

		public string GetName()
		{
			return this.objectName;
		}

        protected virtual void Start()
        {
            Initialization();
        }
        /// <summary>
        /// Finds objects and instances of player
        /// </summary>
        protected void Initialization()
        {
            controller = FindObjectOfType<GameController>();
            iManager = FindObjectOfType<InterfaceManager>();
            image = gameObject.GetComponent<SpriteRenderer>().sprite;
            events = controller.GetEvents();
            player = events.GetPlayer();
            playerMovement = controller.GetMovement();
            objectName = this.gameObject.name;
        }

        protected virtual void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (!player.interacting && iManager.target != this)
                {
                    iManager.SetTarget(this);
                    iManager.SetHolder(dHolder);
                    try
                    {
                        iManager.ShowBox(collisionText, iManager.buttonInteraction, "");
                        player.interacting = true;
                    }
                    catch (System.ArgumentException)
                    {
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                iManager.SetTarget(null);
                iManager.CloseDialogue();
                player.interacting = false;
            }
        }

        public abstract void Interaction();
        /// <summary>
        /// Removes the GameObject, mainly used to remove an NPC after a Bar Fight.
        /// </summary>
        virtual public void Remove()
        {
            Destroy(this.gameObject);
        }
        /// <summary>
        /// Closes Dialogue and sets interacting to false.
        /// </summary>
        public void StopInteracting()
        {
            iManager.CloseDialogue();
            iManager.SetDialogueActive(false);
            player.interacting = false;
        }
    }
}
