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
        private GameObject character, barNPC, storyNPC, doorRWS1, doorMainHall1, doorMainHall2, doorBar;
        private InterfaceManager iManager;
        private GameEvents events;
        private Movement playerMovement;
        private Inventory playerInventory;
        private Text stats, info;

        private void Start()
        {
            events = new GameEvents();
            GetObjects();
            playerMovement = new Movement(player, character);
			iManager.playerMovement = this.playerMovement;
            playerInventory.playerMovement = this.playerMovement;
            playerInventory.player = this.player;
            StartPrefabs();
        }

        public GameEvents GetEvents()
        {
            return events;
        }

        public Movement GetMovement()
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

        /// <summary>
        /// Finds all the prefabs and instantiates them.
        /// </summary>
        private void StartPrefabs()
        {
            barNPC = (GameObject)Resources.Load("BarNPC", typeof(GameObject));
            storyNPC = (GameObject)Resources.Load("NPCBouncer", typeof(GameObject));
            doorRWS1 = (GameObject)Resources.Load("DoorRWS1", typeof(GameObject));
            doorMainHall1 = (GameObject)Resources.Load("DoorMainHall1", typeof(GameObject));
            doorMainHall2 = (GameObject)Resources.Load("DoorMainHall2", typeof(GameObject));
            doorBar = (GameObject)Resources.Load("DoorBar1", typeof(GameObject));

            Instantiate(storyNPC, storyNPC.transform.position, storyNPC.transform.rotation);
            Instantiate(barNPC, barNPC.transform.position, barNPC.transform.rotation);
            Instantiate(doorRWS1, doorRWS1.transform.position, doorRWS1.transform.rotation);
            Instantiate(doorMainHall1, doorMainHall1.transform.position, doorMainHall1.transform.rotation);
            Instantiate(doorMainHall2, doorMainHall2.transform.position, doorMainHall2.transform.rotation);
            Instantiate(doorBar, doorBar.transform.position, doorBar.transform.rotation);
        }

        /// <summary>
        /// Finds all the necessary GameObjects and player from Events.
        /// </summary>
        private void GetObjects()
        {
            character = GameObject.Find("Player");
            stats = GameObject.Find("Stats").GetComponent<Text>();
            info = GameObject.Find("Info").GetComponent<Text>();
            iManager = FindObjectOfType<InterfaceManager>();
            player = events.GetPlayer();
            playerInventory = FindObjectOfType<Inventory>();
        }

    }
}
