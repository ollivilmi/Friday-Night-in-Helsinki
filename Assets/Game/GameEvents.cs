using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Player;
using Dialogue;
using NPC;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameEvents
    {
        private Player.Player player;
        public int score { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        /// <summary>
        /// Creates an instance of the player's character and sets
        /// time from the character's variables.
        /// </summary>
		public GameEvents(string selectedCharacter)
        {
			switch (selectedCharacter) {

			case "Jarno": 
            
				player = new Jarno ();
				break;

			case "Make":
				player = new Make ();
				break;

			case "Teddy":
				player = new Teddy ();
				break;
			}
            this.hour = player.hour;
            this.minute = player.minute;
        }

        public Player.Player GetPlayer()
        {
            return player;
        }

        public void addScore(int score)
        {
            this.score += (int)(score + (0.1 * (double)player.getfunLevel())); //Add score, bonus points for funLevel
        }

        public void ChangeTime(int minute)
        {
            this.minute += minute;
            player.drink(-((double)minute * 1 / 6)); //Drunk level decreases by the amount of minutes passed
            while (this.minute >= 60 || this.hour == 24)
            {
                if (this.minute >= 60)
                {
                    this.minute -= 60;          //Every hour in Africa an hour passes
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

        public string UpdateEvents()
        {
            return "Score: " + score + " Time: " + hour + ":" + minute;
        }

    }
}
