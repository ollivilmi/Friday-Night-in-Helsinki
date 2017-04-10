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
    public class CollisionStoryNPC : MonoBehaviour
    {
        private DialogueHolder dHolder;
        private DialogueManager dManager;
        private NPCBouncer npc;
        private Player.Player player;
        private GameController controller;
        private GameEvents events;

        void Start()
        {
            controller = FindObjectOfType<GameController>();
            dManager = FindObjectOfType<DialogueManager>();
            events = controller.GetEvents();
            player = events.GetPlayer();

            npc = new NPCBouncer();
            dHolder = new DialogueHolderStory(player, dManager, npc);
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                dHolder.touching = true;
                dManager.SetHolder(dHolder);
                try
                {
                    dManager.ShowBox("Talk", dManager.dBoxNPC, "Start");
                } catch (System.ArgumentException ae)
                {
                    Debug.Log(ae);
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
