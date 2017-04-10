using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using Game;
using UnityEngine.UI;
using NPC;
using Player;

namespace Game
{
    public class GameController : MonoBehaviour
    {

        private Player.Player player;
        private GameObject character, barNPC, storyNPC;
        private DialogueManager dManager;
        private GameEvents events;
        private Movement playerMovement;
        private Text stats, info;
        private Vector3 pos;

        void Start()
        {
            barNPC = (GameObject)Resources.Load("BarNPC", typeof(GameObject));
            storyNPC = (GameObject)Resources.Load("NPCBouncer", typeof(GameObject));
            pos = new Vector3(30f, -3.78f, 0f);
            events = new GameEvents();
            character = GameObject.Find("Player");
            stats = GameObject.Find("Stats").GetComponent<Text>();
            info = GameObject.Find("Info").GetComponent<Text>();
            dManager = FindObjectOfType<DialogueManager>();
            player = events.GetPlayer();
            playerMovement = new Movement(player, dManager, character);

            Instantiate(storyNPC, storyNPC.transform.position, storyNPC.transform.rotation);
            Instantiate(barNPC, barNPC.transform.position, barNPC.transform.rotation);
        }

        public GameEvents GetEvents()
        {
            return events;
        }

        void Update()
        {
            playerMovement.charPos = character.transform.position;
            playerMovement.LeftClick();
            info.text = events.UpdateEvents();
            stats.text = player.UpdateStats();
        }
    }
}
