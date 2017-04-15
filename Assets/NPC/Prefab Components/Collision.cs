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
                iManager.SetTarget(this);
                try
                {
                    iManager.ShowBox(collisionText, iManager.buttonInteraction, "Start");
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
            }
        }

        public abstract void Interaction();
    }
}
