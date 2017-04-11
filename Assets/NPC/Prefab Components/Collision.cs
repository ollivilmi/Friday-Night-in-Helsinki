using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;

namespace NPC
{
    public class Collision : MonoBehaviour
    {
        protected DialogueHolder dHolder;
        protected DialogueManager dManager;
        protected Player.Player player;
        protected GameController controller;
        protected GameEvents events;
        protected string collisionText;
   
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                dHolder.touching = true;
                dManager.SetHolder(dHolder);
                try
                {
                    dManager.ShowBox(collisionText, dManager.dBoxNPC, "Start");
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
                dHolder.touching = false;
                dManager.CloseDialogue();
            }
        }
    }
}
