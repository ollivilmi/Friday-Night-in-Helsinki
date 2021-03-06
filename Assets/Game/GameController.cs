﻿using System.Collections;
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
        private GameObject character, npcFiller, npcBar, npcDrunk;
        private List<GameObject> triggerObjects;
        private InterfaceManager iManager;
        private GameEvents events;
        private Movement playerMovement;
        private Inventory playerInventory;
        private Text info;
        private Door door;
        private NPCType npcType;
        private Tahti tahti;
        private Blackjack blackjack;
		private DataSaver dataSaver;
        private BarFight barfight;
        private Cutscene cutscene;
        public int npcBarCount { get; set; }
		private string selectedCharacter;
		public bool moving { get; set; }
        private System.Random random;

        private void Start()
        {
            random = new System.Random();
            dataSaver = FindObjectOfType<DataSaver> ();
			selectedCharacter = dataSaver.character;   
            getObjects();
            events = new GameEvents(selectedCharacter, iManager, cutscene, character, this);
            player = events.GetPlayer();
            setUpScripts();
            startPrefabs();     
        }
        /// <summary>
        /// Get GameEvents object
        /// </summary>
        /// <returns></returns>
        public GameEvents GetEvents()
        {
            return events;
        }
        /// <summary>
        /// Get Player's movement script (used mainly to stop movement)
        /// </summary>
        /// <returns></returns>
        public Movement GetMovement()
        {
            return playerMovement;
        }
        /// <summary>
        /// Get player object
        /// </summary>
        /// <returns></returns>
        public Player.Player GetPlayer()
        {
            return player;
        }

        private void Update()
        {
            playerMovement.posChar = character.transform.position;
            playerMovement.LeftClick();
            info.text = events.GetClock() + player.UpdateStats();
			handleAnimations ();
            if (npcBarCount < 5)
            {
                StartCoroutine(spawnBarNPC());
            }
            if (player.drunkLevel == 100)
            {
                StartCoroutine(cutscene.CutsceneBlackout());
            }
        }

        /// <summary>
        /// Finds all the prefabs and instantiates them.
        /// </summary>
        private void startPrefabs()
        {
            triggerObjects = new List<GameObject> {
                (GameObject)Resources.Load("NPCPetri", typeof(GameObject)),
                (GameObject)Resources.Load("NPCMatti", typeof(GameObject)),
                (GameObject)Resources.Load("NPCTommi", typeof(GameObject)),
                (GameObject)Resources.Load("NPCHeikki", typeof(GameObject)),
                (GameObject)Resources.Load("NPCJartsa", typeof(GameObject)),
                (GameObject)Resources.Load("NPCAlexander", typeof(GameObject)),
                (GameObject)Resources.Load("NPCLiinu", typeof(GameObject)),
                (GameObject)Resources.Load("NPCJ-P", typeof(GameObject)),
                (GameObject)Resources.Load("NPCSpaceman", typeof(GameObject)),
                (GameObject)Resources.Load("NPCKake", typeof(GameObject)),
                (GameObject)Resources.Load("DoorRWS1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorRWSCasino", typeof(GameObject)),
                (GameObject)Resources.Load("DoorRWSNightClub", typeof(GameObject)),
                (GameObject)Resources.Load("DoorCasino", typeof(GameObject)),
                (GameObject)Resources.Load("DoorNightClub", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall2", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMainHall3", typeof(GameObject)),
                (GameObject)Resources.Load("DoorBar1", typeof(GameObject)),
                (GameObject)Resources.Load("DoorMetroHelsinki1", typeof(GameObject)),
                (GameObject)Resources.Load("MetroHelsinki", typeof(GameObject)),
                (GameObject)Resources.Load("MetroSornainen", typeof(GameObject)),
            };

            foreach (GameObject element in triggerObjects)
            {
                Instantiate(element, element.transform.position, element.transform.rotation);
            }

            npcFiller = (GameObject)Resources.Load("NPCFiller", typeof(GameObject));
			Instantiate(npcFiller, npcFiller.transform.position, npcFiller.transform.rotation);
            Instantiate(npcFiller, new Vector3(-90f, -8.3f), npcFiller.transform.rotation);
            npcBar = (GameObject)Resources.Load("BarNPC", typeof(GameObject));
            npcBarCount = 0;
            npcDrunk = (GameObject)Resources.Load("NPCDrunk", typeof(GameObject));
            Instantiate(npcDrunk, npcDrunk.transform.position, npcDrunk.transform.rotation);
            Instantiate(npcDrunk, new Vector3(111f, -7.3f, 0), npcDrunk.transform.rotation);
            Instantiate(npcDrunk, new Vector3(-614f, -7.3f, 0), npcDrunk.transform.rotation);
        }            
        /// <summary>
        /// Spawns bar NPCs after a 30 second delay if npcBarCount is less than 5
        /// </summary>
        /// <returns></returns>
        private IEnumerator spawnBarNPC()
        {
            npcBarCount++;
            yield return new WaitForSeconds(30);
            Instantiate(npcBar, new Vector3((float)random.Next(460,485), -random.Next(5,8)), npcBar.transform.rotation);      
        }

        /// <summary>
        /// Finds all the necessary GameObjects and player from Events.
        /// </summary>
        private void getObjects()
        {
            character = GameObject.Find("Player");
            info = GameObject.Find("Info").GetComponent<Text>();
            door = FindObjectOfType<Door>();
            npcType = FindObjectOfType<NPCType>();
            tahti = FindObjectOfType<Tahti>();
            blackjack = FindObjectOfType<Blackjack>();
            iManager = FindObjectOfType<InterfaceManager>();
            cutscene = FindObjectOfType<Cutscene>();
            barfight = FindObjectOfType<BarFight>();
            playerInventory = FindObjectOfType<Inventory>();
        }

        /// <summary>
        /// Creates an instance of movement, sets up script variables.
        /// </summary>
        private void setUpScripts()
        {
            playerMovement = new Movement(player, character);
            iManager.playerMovement = this.playerMovement;
            playerInventory.playerMovement = this.playerMovement;
            playerInventory.player = this.player;
            door.player = this.player;
            door.events = this.events;
            npcType.player = this.player;
            tahti.player = this.player;
            tahti.playerMovement = this.playerMovement;
            cutscene.SetPlayer(player.GetPlayerSprite());
            cutscene.events = this.events;
            iManager.imagePlayer.sprite = player.GetPlayerSprite();
            blackjack.player = this.player;
            blackjack.playerMovement = this.playerMovement;
            barfight.player = this.player;
            barfight.SetPlayerSprite(player.playerSprite);
            FindObjectOfType<GuitarGod>().playerMovement = this.playerMovement;
        }
		/// <summary>
		/// Handles player character's animations.
		/// Is called in Update();
		/// </summary>
		private void handleAnimations()
		{
			//checks if player is moving
			if (playerMovement.moveLeft == true || playerMovement.moveRight == true) 
			{
				player.playerAnimator.SetBool ("moving", true);
			} 
			else 
			{
				player.playerAnimator.SetBool ("moving", false);
			}
		}
        /// <summary>
        /// Changes skybox color for different game times
        /// </summary>
        /// <param name="hour">Current time in the game</param>
        public void ChangeSkyBox(int hour)
        {
            if (hour < 19 && hour > 8)
            {
                Camera.main.backgroundColor = new Color(86f/255, 111f/255, 150f/255);
                return;
            }
            else if (hour < 20 && hour > 8)
            {
                Camera.main.backgroundColor = new Color(46f/255, 87f/255, 150f/255);
                return;
            }
            else if (hour < 21 && hour > 8)
            {
                Camera.main.backgroundColor = new Color(0, 25f/255, 64f/255);
                return;
            }
            else if (hour < 22 && hour > 8)
            {
                Camera.main.backgroundColor = new Color(0, 12f/255, 30f/255);
                return;
            }
            else if (hour >= 22 || hour <= 8)
            {
                Camera.main.backgroundColor = new Color(0, 5f/255, 10f/255);
                return;
            }
        }
    }
}
