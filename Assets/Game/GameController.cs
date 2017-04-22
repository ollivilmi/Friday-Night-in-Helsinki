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
        private GameObject character, npcFiller;
        private List<GameObject> triggerObjects;
        private InterfaceManager iManager;
        private GameEvents events;
        private Movement playerMovement;
        private Inventory playerInventory;
        private Text stats, info;
        private Door door;
        private NPCType npcType;
		private DataSaver dataSaver;
		private string selectedCharacter;

        private void Start()
        {
			dataSaver = FindObjectOfType<DataSaver> ();
			selectedCharacter = dataSaver.character;
            events = new GameEvents(selectedCharacter);
            GetObjects();
            SetUpScripts();
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
            triggerObjects = new List<GameObject> {
                (GameObject)Resources.Load("BarNPC", typeof(GameObject)),
                (GameObject)Resources.Load("NPCPetri", typeof(GameObject)),
                (GameObject)Resources.Load("NPCMatti", typeof(GameObject)),
                (GameObject)Resources.Load("NPCFiller", typeof(GameObject)),
                (GameObject)Resources.Load("NPCTommi", typeof(GameObject)),
                (GameObject)Resources.Load("DoorRWS1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall2", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall3", typeof(GameObject)),
                (GameObject)Resources.Load("DoorBar1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMetroHelsinki1", typeof(GameObject)),
                (GameObject)Resources.Load("MetroHelsinki", typeof(GameObject)),
                (GameObject)Resources.Load("MetroSornainen", typeof(GameObject))
            };

            foreach (GameObject element in triggerObjects)
            {
                Instantiate(element, element.transform.position, element.transform.rotation);
            }

            npcFiller = (GameObject)Resources.Load("NPCFiller", typeof(GameObject));
            Instantiate(npcFiller, new Vector3(-90f, -8.3f), npcFiller.transform.rotation);
        }

        /// <summary>
        /// Finds all the necessary GameObjects and player from Events.
        /// </summary>
        private void GetObjects()
        {
            character = GameObject.Find("Player");
            stats = GameObject.Find("Stats").GetComponent<Text>();
            info = GameObject.Find("Info").GetComponent<Text>();
            door = FindObjectOfType<Door>();
            npcType = FindObjectOfType<NPCType>();
            iManager = FindObjectOfType<InterfaceManager>();
            player = events.GetPlayer();
            playerInventory = FindObjectOfType<Inventory>();
        }

        /// <summary>
        /// Creates an instance of movement, sets up script variables.
        /// </summary>
        private void SetUpScripts()
        {
            playerMovement = new Movement(player, character);
            iManager.playerMovement = this.playerMovement;
            playerInventory.playerMovement = this.playerMovement;
            playerInventory.player = this.player;
            door.player = this.player;
            door.events = this.events;
            npcType.player = this.player;
        }
    }
}
