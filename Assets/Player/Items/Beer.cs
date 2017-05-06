using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Player
{
    public class Beer : Item
    {
        /// <summary>
        /// Adds a beer to the player's item list.
        /// Price isn't currently used in the game, but it could be used
        /// to sell the item.
        /// </summary>
        /// <param name="player">Player object, which contains all the information about the player.</param>
        /// <param name="amount">How many beers the player starts with.</param>
        public Beer(Player player, int amount)
        {
            this.player = player;
            this.price = 1.1f;
            this.amount = amount;
            this.itemName = "Beer";
        }
        /// <summary>
        /// Uses a player's beer if the player has one. Increases drunk level by 10.
        /// If drunkLevel is over 100, returns false to indicate that the player is too drunk.
        /// Also returns false if player doesn't have any beers left.
        /// </summary>
        /// <returns></returns>
        public override bool UseItem()
        {
            if (amount > 0)
            {
                amount--;
                player.drink(10);
                if (player.drunkLevel < 100)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
