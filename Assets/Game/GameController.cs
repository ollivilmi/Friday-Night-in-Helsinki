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
        private GameObject character, npcFiller, npcBar;
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
		public bool moving;
        private System.Random random;

        private void Start()
        {
            random = new System.Random();
			dataSaver = FindObjectOfType<DataSaver> ();
			selectedCharacter = dataSaver.character;   
            GetObjects();
            events = new GameEvents(selectedCharacter, iManager, cutscene, character);
            player = events.GetPlayer();
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

        public Player.Player GetPlayer()
        {
            return player;
        }

        private void Update()
        {
            playerMovement.posChar = character.transform.position;
            playerMovement.LeftClick();
            info.text = events.GetClock() + player.UpdateStats();
			HandleAnimations ();
            if (npcBarCount < 5)
            {
                StartCoroutine(SpawnBarNPC());
            }
            if (player.drunkLevel == 100)
            {
                StartCoroutine(cutscene.CutsceneBlackout());
            }
        }

        /// <summary>
        /// Finds all the prefabs and instantiates them.
        /// </summary>
        private void StartPrefabs()
        {
            triggerObjects = new List<GameObject> {
                (GameObject)Resources.Load("NPCPetri", typeof(GameObject)),
                (GameObject)Resources.Load("NPCMatti", typeof(GameObject)),
                (GameObject)Resources.Load("NPCTommi", typeof(GameObject)),
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
        }            

        private IEnumerator SpawnBarNPC()
        {
            npcBarCount++;
            yield return new WaitForSeconds(30);
            Instantiate(npcBar, new Vector3((float)random.Next(460,485), -random.Next(5,8)), npcBar.transform.rotation);      
        }

        /// <summary>
        /// Finds all the necessary GameObjects and player from Events.
        /// </summary>
        private void GetObjects()
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
        private void SetUpScripts()
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
            cutscene.player.GetComponent<Image>().sprite = player.GetPlayerSprite();
            cutscene.events = this.events;
            iManager.imagePlayer.sprite = player.GetPlayerSprite();
            blackjack.player = this.player;
            blackjack.playerMovement = this.playerMovement;
            barfight.player = this.player;
            barfight.SetPlayerSprite(player.playerSprite);
        }
		/// <summary>
		/// Handles player character's animations.
		/// Is called in Update();
		/// </summary>
		void HandleAnimations()
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
    }
}
