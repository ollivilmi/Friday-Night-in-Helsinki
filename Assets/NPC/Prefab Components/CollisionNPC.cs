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
    public class CollisionNPC : MonoBehaviour
    {
        protected DialogueHolder dHolder;
        protected DialogueManager dManager;
        protected Player.Player player;
        protected GameController controller;
        protected GameEvents events;
   
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                dHolder.touching = true;
                dManager.SetHolder(dHolder);
                dManager.ShowBox("Talk", dManager.dBoxNPC, "Start");
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
