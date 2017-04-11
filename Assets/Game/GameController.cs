using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using Game;
using UnityEngine.UI;
using NPC;
using Player;
using Interface;

namespace Game
{
    public class GameController : MonoBehaviour
    {

        private Player.Player player;
        private GameObject character, barNPC, storyNPC, doorTrigger;
        private InterfaceManager iManager;
        private GameEvents events;
        private Movement playerMovement;
        private Text stats, info;

        private void Start()
        {
            barNPC = (GameObject)Resources.Load("BarNPC", typeof(GameObject));
            storyNPC = (GameObject)Resources.Load("NPCBouncer", typeof(GameObject));
            doorTrigger = (GameObject)Resources.Load("DoorTrigger", typeof(GameObject));
            events = new GameEvents();
            character = GameObject.Find("Player");
            stats = GameObject.Find("Stats").GetComponent<Text>();
            info = GameObject.Find("Info").GetComponent<Text>();
            iManager = FindObjectOfType<InterfaceManager>();
            player = events.GetPlayer();
            playerMovement = new Movement(player, iManager, character);

            Instantiate(storyNPC, storyNPC.transform.position, storyNPC.transform.rotation);
            Instantiate(barNPC, barNPC.transform.position, barNPC.transform.rotation);
            Instantiate(doorTrigger, doorTrigger.transform.position, doorTrigger.transform.rotation);
        }

        public GameEvents GetEvents()
        {
            return events;
        }

        public Movement getMovement()
        {
            return playerMovement;
        }

        private void Update()
        {
            playerMovement.posChar = character.transform.position;
            playerMovement.LeftClick();
            info.text = events.UpdateEvents();
            stats.text = player.UpdateStats();
        }
    }
}
