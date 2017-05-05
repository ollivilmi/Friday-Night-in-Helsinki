using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Player;
using Dialogue;
using NPC;
using UnityEngine;
using UnityEngine.UI;
using Interface;

namespace Game
{
    public class GameEvents
    {
        private Player.Player player;
        public int score { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
		public string selectedCharacter { get; set; }
        public InterfaceManager iManager { get; set; }
        public Cutscene cutscene { get; set; }
        public GameController controller { get; set; }

        /// <summary>
        /// Creates an instance of the player's character and sets
        /// time from the character's variables.
        /// </summary>
		public GameEvents(string selectedCharacter, InterfaceManager iManager, Cutscene cutscene, GameObject character, GameController controller)
        {
			this.selectedCharacter = selectedCharacter;
            this.iManager = iManager;
            this.cutscene = cutscene;
            this.controller = controller;

            switch (selectedCharacter) {
			case "Jarno": 
				player = new Jarno ();
                character.transform.position = new Vector2(character.transform.position.x, player.height);
                    break;
			case "Make":
				player = new Make ();
                character.transform.position = new Vector2(character.transform.position.x, player.height);
                    break;
			case "Teddy":
				player = new Teddy ();
                character.transform.position = new Vector2(character.transform.position.x, player.height);
				    break;
			}
            this.hour = player.hour;
            this.minute = player.minute;
            controller.ChangeSkyBox(hour);
            player.events = this;
        }
        /// <summary>
        /// Returns player
        /// </summary>
        /// <returns></returns>
        public Player.Player GetPlayer()
        {
            return player;
        }
        /// <summary>
        /// Adds to the score variable
        /// </summary>
        /// <param name="score">Variable which is used to track how well you have done.</param>
        public void addScore(int score)
        {
            this.score += (int)(score + (0.1 * (double)player.getfunLevel())); //Add score, bonus points for funLevel
        }
        /// <summary>
        /// Used to manipulate time. Decreases drunkLevel and funLevel as time passes, and changes SkyBox
        /// to become darker as it gets late.
        /// </summary>
        /// <param name="minute">How many minutes have passed</param>
        public void ChangeTime(int minute)
        {
            this.minute += minute;
            player.drink(-((double)minute * 1 / 6)); //Drunk level decreases by the amount of minutes passed
            while (this.minute >= 60 || this.hour == 24)
            {
                iManager.OpenPopUp("Another hour has passed.");
                controller.ChangeSkyBox(hour);
                if (this.minute >= 60)
                {
                    this.minute -= 60;
                    this.hour++;                // Have some fun if you are drunk
                    player.haveFun(-10);        // Fun level decreases over time	
                    score += (player.getfunLevel()) / 2;
                }
                if (this.hour == 24)
                {
                    this.hour = 0;
                }
            }
        }
        /// <summary>
        /// Prints clock in the correct format.
        /// </summary>
        /// <returns></returns>
        public string GetClock()
        {
            string time = "" + hour +":";
            if (minute < 10)
            {
                time += "0" + minute;
            }
            else time += minute;
            return "Time: " +time;
        }

    }
}
