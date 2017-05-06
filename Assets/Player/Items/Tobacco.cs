using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Tobacco : Item
    {
        /// <summary>
        /// Adds Tobacco to the player's item list.
        /// </summary>
        /// <param name="player">Player object.</param>
        /// <param name="amount">How many tobaccos the player starts with.</param>
        public Tobacco(Player player, int amount)
        {
            this.price = 0.2f;
            this.amount = amount;
            this.itemName = "Tobacco";
            this.player = player;
        }
        /// <summary>
        /// If the player has Tobacco, uses it to have some fun.
        /// Returns false if the player doesn't have any.
        /// </summary>
        /// <returns></returns>
        override public bool UseItem()
        {
            if (amount > 0)
            {
                amount--;
                player.haveFun(2);
                return true;
            }
            return false;
        }
    }
}
