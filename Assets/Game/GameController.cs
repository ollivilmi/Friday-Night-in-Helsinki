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
        private GameObject character;
        private GameObject npc;
        private DialogueManager dMan; //TODO: FIX DIALOGUE BUG CLICKING NPC TWICE
        private GameEvents events;
        private Movement playerMovement;
        private Text stats, info;
        private DialogueHolder dHolder;
        private Vector3 pos;

        void Start()
        {
            npc = (GameObject)Resources.Load("NPC", typeof(GameObject));
            pos = new Vector3(30f, -3.78f, 0f);
            events = new GameEvents();
            character = GameObject.Find("Player");
            stats = GameObject.Find("Stats").GetComponent<Text>();
            info = GameObject.Find("Info").GetComponent<Text>();
            dMan = FindObjectOfType<DialogueManager>();
            player = events.GetPlayer();
            playerMovement = new Movement(player, dMan, character);
            Instantiate(npc, npc.transform.position, npc.transform.rotation);
            Instantiate(npc, pos, npc.transform.rotation);
        }

        public GameEvents GetEvents()
        {
            return events;
        }

        void Update()
        {
            playerMovement.charPos = character.transform.position; //Get character position in the world
            playerMovement.LeftClick();
            info.text = events.UpdateEvents();
            stats.text = player.UpdateStats();
        }
    }
}
