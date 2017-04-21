using System.Collections;
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
            objectName = this.gameObject.name;
        }
   
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (!player.interacting)
                {
                    iManager.SetTarget(this);
                    try
                    {
                        iManager.ShowBox(collisionText, iManager.buttonInteraction, "Start");
                        player.interacting = true;
                    }
                     catch (System.ArgumentException)
                    {
                    }
                }
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D col)
        {
            if (!player.interacting)
            {
                iManager.SetTarget(this);
                try
                {
                    iManager.ShowBox(collisionText, iManager.buttonInteraction, "Start");
                    player.interacting = true;
                }
                catch (System.ArgumentException)
                {
                }
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                iManager.CloseDialogue();
                player.interacting = false;
            }
        }

        public abstract void Interaction();
    }
}
