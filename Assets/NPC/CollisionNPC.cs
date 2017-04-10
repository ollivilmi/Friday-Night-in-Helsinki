using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Dialogue;
using NPC;
using Game;
using Player;

namespace NPC {
    public class CollisionNPC : MonoBehaviour {
        private DialogueHolder dHolder;
        private DialogueManager dManager;
        private BarNPC barNPC;
        private Player.Player player;
        private GameController controller;
        private GameEvents events;

        void Start()
        {
            controller = FindObjectOfType<GameController>();
            dManager = FindObjectOfType<DialogueManager>();
            events = controller.GetEvents();
            player = events.GetPlayer();

            barNPC = new BarNPC();
            dHolder = new DialogueHolder(player, dManager, barNPC);
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                dHolder.touching = true;
                dManager.SetHolder(dHolder);
                dManager.ShowBox("Talk", dManager.dNPC, "Start");
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dHolder.moodChange != 0)
            {
                barNPC.changeMood(dHolder.ReturnMoodChange());
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
